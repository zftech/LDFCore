using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Common.UploadFile.Model
{
   public class FileAttachment
    {
        public string Code { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExt { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }
        /// <summary>
        /// 是否为临时文件
        /// </summary>
        public bool IsTemprory { get; set; }

        /// <summary>
        /// 文件保存模式
        /// </summary>
        public SaveFileModeEnum? SaveFileMode { get; set; }
        /// <summary>
        /// dfs组名
        /// </summary>
        public string GroupName { get; set; }
    }
}
