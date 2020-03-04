using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LDFCore.Common.UploadFile.Model
{
    public enum SaveFileModeEnum
    {
        /// <summary>
        /// 本地
        /// </summary>
        [Description("本地")]
        Local = 0,
        /// <summary>
        /// DFS
        /// </summary>
        [Description("DFS")]
        DFS = 1
    }
}
