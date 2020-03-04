using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LDFCore.Authorized.Enums
{
    public enum PlatformEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = 0,

        /// <summary>
        /// Web
        /// </summary>
        [Description("Web")]
        Web = 1,

        /// <summary>
        /// 安卓
        /// </summary>
        [Description("安卓")]
        Android = 2,

        /// <summary>
        /// IOS
        /// </summary>
        [Description("IOS")]
        IOS = 3,
        /// <summary>
        /// 小程序
        /// </summary>
        [Description("小程序")]
        MiniProgram = 4,

        /// <summary>
        /// 公众号
        /// </summary>
        [Description("公众号")]
        MPWeixin = 5
    }
}
