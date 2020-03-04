using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Common.FileUpload.Configuration
{
    public class FileUploadOption
    {
        /// <summary>
        /// 文件上传根目录
        /// </summary>
        public string Root { get; set; }

        /// <summary>
        /// 文件请求路径
        /// </summary>
        public string RequestPath { get; set; }

        /// <summary>
        /// 上传文件分类
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 签章根目录
        /// </summary>
        public string SignatureRoot { get; set; }

        /// <summary>
        /// 项目附件压缩文件路径
        /// </summary>
        public string ZipFilePath { get; set; }

        /// <summary>
        /// 上传路由
        /// </summary>
        public string RequestRoute { get; set; }

        /// <summary>
        /// 支持的文件类型
        /// </summary>
        public Dictionary<string, string> SupportType { get; set; }

    }


}
