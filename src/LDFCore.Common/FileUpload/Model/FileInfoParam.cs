using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LDFCore.Common.FileUpload.Model
{
   public class FileInfoParam
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string FileExtion { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 远程地址
        /// </summary>
        public string RemoteUrl { get; set; }

        /// <summary>
        /// 本地地址
        /// </summary>
        public string LocalUrl { get; set; }

        /// <summary>
        /// 图片款
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 图片高
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 所属模块
        /// </summary>
        public string Module { get; set; }
    }

    public enum FileType
    {
        /// <summary>
        ///  压缩文件
        /// </summary>
        [Description("压缩文件")]
        Zip,
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Image,
        /// <summary>
        /// 文本
        /// </summary>
        [Description("文本")]
        Text,
        /// <summary>
        /// 文档
        /// </summary>
        [Description("文档")]
        Word,
        /// <summary>
        /// 演示文稿
        /// </summary>
        [Description("演示文稿")]
        PPT,
        /// <summary>
        /// 表格
        /// </summary>
        [Description("表格")]
        Excel,
        /// <summary>
        /// PDF
        /// </summary>
        [Description("PDF")]
        PDF,
        /// <summary>
        /// 未知文件
        /// </summary>
        [Description("未知文件")]
        UnKnow,
        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频")]
        Video,
        /// <summary>
        /// 音频
        /// </summary>
        [Description("音频")]
        Audio,

        /// <summary>
        /// 可执行文件
        /// </summary>
        [Description("可执行文件")]
        Executable,

        /// <summary>
        /// cad
        /// </summary>
        [Description("CAD")]
        CAD
    }
}
