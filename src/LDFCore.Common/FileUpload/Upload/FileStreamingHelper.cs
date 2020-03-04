using LDFCore.Common.FileUpload.Configuration;
using LDFCore.Common.FileUpload.Model;
using LDFCore.Platform.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LDFCore.Common.FileUpload.Upload
{
    public static class FileStreamingHelper
    {        

        /// <summary>
        /// 如果文件上传成功，那么message会返回一个上传文件的路径，如果失败，message代表失败的消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="targetDirectory"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<(bool success, IResultModel result, FormValueProvider valueProvider)> StreamFile(this HttpRequest request, FileUploadOption option, CancellationToken cancellationToken)
        {
            //读取boundary

            var boundary = request.GetMultipartBoundary();
            if (string.IsNullOrEmpty(boundary))
            {
                return (false, ResultModel.Failed("解析失败"), null);
            }
            //检查相应目录
            var childDir = Path.Combine(option.Module, DateTime.Now.ToString("yyyyMM"));
            var fileDirectory = Path.Combine(option.Root, childDir);
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            //准备文件保存路径
            var fileReqestPath = string.Empty;
            var physicalFilefilePath = string.Empty;
            //准备viewmodel缓冲
            var accumulator = new KeyValueAccumulator();
            //创建section reader
            var reader = new MultipartReader(boundary, request.Body);
            FileInfoParam fileInfo = new FileInfoParam();
            try
            {
                var section = await reader.ReadNextSectionAsync(cancellationToken);
                while (section != null)
                {
                    ContentDispositionHeaderValue header = section.GetContentDispositionHeader();
                    if (header.FileName.HasValue || header.FileNameStar.HasValue)
                    {
                        var fileSection = section.AsFileSection();

                        //检测上传类型
                        var originalfileName = header.FileName.HasValue ? header.FileName.Value : "";
                        if (!originalfileName.CheckExtesionSupportType(option) || !fileSection.FileStream.CheckSupportType(option))
                        {
                            var errorExtension = "";
                            if (header.FileName.HasValue)
                            {
                                errorExtension = Path.GetExtension(header.FileName.Value);
                            }
                            return (false, ResultModel.Failed($"文件类型不支持上传:{errorExtension}"), null);
                        }


                        fileInfo = GetFileInfo(fileSection);
                        //var fileName = fileSection.FileName;
                        var fileName = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(fileSection.FileName)}";
                        fileReqestPath = Path.Combine(option.RequestPath, childDir, fileName);
                        physicalFilefilePath = Path.Combine(fileDirectory, fileName);
                        if (File.Exists(physicalFilefilePath))
                        {
                            return (false, ResultModel.Failed("你以上传过同名文件"), null);
                        }
                        accumulator.Append("mimeType", fileSection.Section.ContentType);
                        accumulator.Append("fileName", fileName);
                        accumulator.Append("filePath", fileReqestPath);
                        //using (var writeStream = File.Create(filePath))
                        using (var writeStream = File.Create(physicalFilefilePath))
                        {
                            const int bufferSize = 1024;
                            await fileSection.FileStream.CopyToAsync(writeStream, bufferSize, cancellationToken);
                        }
                    }
                    else
                    {
                        var formDataSection = section.AsFormDataSection();
                        var name = formDataSection.Name;
                        var value = await formDataSection.GetValueAsync();
                        accumulator.Append(name, value);
                    }
                    section = await reader.ReadNextSectionAsync(cancellationToken);
                }
                fileInfo.LocalUrl = fileReqestPath;
            }
            catch (OperationCanceledException)
            {
                if (File.Exists(physicalFilefilePath))
                {
                    File.Delete(physicalFilefilePath);
                }
                return (false, ResultModel.Failed("用户取消操作"), null);
            }

            // Bind form data to a model
            var formValueProvider = new FormValueProvider(
               BindingSource.Form,
               new FormCollection(accumulator.GetResults()),
               CultureInfo.CurrentCulture);
            return (true, ResultModel.Success(fileInfo), formValueProvider);

        }
        
        
        public static bool CheckSupportType(this Stream stream, FileUploadOption _option)
        {
            if (_option.SupportType == null || !_option.SupportType.Any())
            {
                return true;
            }
            var fileClass = GetFileClass(stream);
            if (_option.SupportType.Any(x => x.Value.Equals(fileClass)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通过后缀检测文件是否支持上传
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="_option"></param>
        /// <returns></returns>
        public static bool CheckExtesionSupportType(this string fileName, FileUploadOption _option)
        {
            if (_option.SupportType == null || !_option.SupportType.Any())
            {
                return true;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            var extesion = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extesion))
            {
                return true;
            }
            else
            {
                extesion = extesion.Substring(1);
                return _option.SupportType.Any(x => x.Key.Contains(extesion));
            }
        }

        /// <summary>
        /// 根据文件流前两个字节得出对应的值
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static string GetFileClass(Stream stream)
        {
            var position = stream.Position;
            BinaryReader reader = new BinaryReader(stream);
            string fileclass = "";
            try
            {
                for (int i = 0; i < 2; i++)
                {
                    fileclass += reader.ReadByte().ToString();
                }
                stream.Position = position;
                return fileclass;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        public static FileInfoParam GetFileInfo(FileMultipartSection fileMultipartSection)
        {
            FileInfoParam fileInfo = new FileInfoParam()
            {
                FileName = fileMultipartSection.FileName,
                // FileName = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(fileMultipartSection.FileName)}",
                Size = fileMultipartSection.FileStream.Length,
                FileExtion = Path.GetExtension(fileMultipartSection.FileName)
            };
            fileInfo.FileType = GetFileType(fileInfo.FileExtion);
            return fileInfo;
        }

        public static FileType GetFileType(string extion)
        {
            switch (extion.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                    return FileType.Image;
                case ".zip":
                case ".rar":
                    return FileType.Zip;
                case ".mp3":
                case ".waw":
                    return FileType.Audio;
                case ".xls":
                case ".xlsx":
                    return FileType.Excel;
                case ".pdf":
                    return FileType.PDF;
                case ".ppt":
                case ".pptx":
                    return FileType.PPT;
                case ".txt":
                    return FileType.Text;
                case ".mp4":
                case ".avi":
                case ".rm":
                    return FileType.Video;
                case ".doc":
                case ".docx":
                    return FileType.Word;
                case ".exe":
                case ".mis":
                    return FileType.Executable;
                case ".dwg":
                case ".dxf":
                case ".dwt":
                    return FileType.CAD;
                default:
                    return FileType.UnKnow;
            }
        }
    }
}
