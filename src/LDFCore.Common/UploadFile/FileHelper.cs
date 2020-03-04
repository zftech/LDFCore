using LDFCore.Common.UploadFile.Configuration;
using LDFCore.Common.UploadFile.Model;
using LDFCore.Platform.Extensions;
using LDFCore.Platform.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LDFCore.Common.UploadFile
{
    public class FileHelper
    {

        /// <summary>
        /// 通过FileAttachmentVM获取其文件流
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public static byte[] GetFileByteForDownLoadByVM(FileAttachment vm, FileUploadOptions  fileUploadOptions)
        {
            byte[] data = null;
            SaveFileModeEnum saveMode = vm.SaveFileMode == null ? fileUploadOptions.SaveFileMode : vm.SaveFileMode.Value;
            data = GetBytes(saveMode, vm);
            return data;
        }

        /// <summary>
        /// 上传文件并返回FileAttachment(后台代码使用)
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="FileData"></param>
        /// <param name="con"></param>
        /// <param name="FileName"></param>
        /// <param name="savePlace"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static async Task<IResultModel> GetFileByteForUploadAsync(Stream FileData, FileUploadOptions fileUploadOptions, string FileName, string module, string groupName = null)
        {
            FileAttachment vm = new FileAttachment();
            var ext = string.Empty;
            if (string.IsNullOrEmpty(FileName) == false)
            {
                var dotPos = FileName.LastIndexOf('.');
                ext = FileName.Substring(dotPos + 1);
            }
            vm.FileName = FileName;
            vm.FileExt = ext; 
            vm.Length = FileData.Length;
            vm.UploadTime = DateTime.Now;
            if (fileUploadOptions.SaveFileMode == SaveFileModeEnum.Local)
            {
                string pathHeader = Path.Combine(Path.Combine(fileUploadOptions.UploadDir,module),DateTime.Now.ToString("yyyyMM"));
                if (!Directory.Exists(pathHeader))
                {
                    Directory.CreateDirectory(pathHeader);
                }
                vm.Code = Guid.NewGuid().ToNoSplitString();
                var fullPath = Path.Combine(pathHeader, $"{vm.Code}.{vm.FileExt}");
                using (var fileStream = File.Create(fullPath))
                {
                   await FileData.CopyToAsync(fileStream);
                    vm.Path = fullPath;
                }
            }
            else if (fileUploadOptions.SaveFileMode == SaveFileModeEnum.DFS)
            {
                throw new Exception("未实现");
                //using (var dataStream = new MemoryStream())
                //{
                //    StorageNode node = null;
                //    FileData.CopyTo(dataStream);

                //    if (!string.IsNullOrEmpty(groupName))
                //    {
                //        node = FDFSClient.GetStorageNode(groupName);
                //    }
                //    else
                //    {
                //        node = FDFSClient.GetStorageNode();
                //    }

                //    if (node != null)
                //    {
                //        vm.Entity.Path = "/" + FDFSClient.UploadFile(node, dataStream.ToArray(), vm.Entity.FileExt);
                //        vm.Entity.GroupName = node.GroupName;
                //    }
                //    vm.Entity.FileData = null;

                //}
                //FileData.Dispose();
            }
            return ResultModel.Success(vm);
        }


        /// <summary>
        /// 获取附件字节数组
        /// </summary>
        /// <param name="saveMode"></param>
        /// <param name="fa"></param>
        /// <param name="DC"></param>
        /// <returns></returns>
        private static byte[] GetBytes(SaveFileModeEnum saveMode, FileAttachment fa)
        {
            byte[] data = null;
            switch (saveMode)
            {
                case SaveFileModeEnum.Local:
                    if (!string.IsNullOrEmpty(fa.Path) && File.Exists(fa.Path))
                    {
                        data = File.ReadAllBytes(fa.Path);
                    }
                    break;
                case SaveFileModeEnum.DFS:
                    new Exception("未实现");
                    //try
                    //{
                    //    data = FDFSClient.DownloadFile(fa.GroupName, fa.Path.TrimStart('/'));
                    //}
                    //catch (FDFSException)
                    //{
                    //}
                    break;
            }
            return data;
        }
    }
}
