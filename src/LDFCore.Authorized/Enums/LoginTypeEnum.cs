using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LDFCore.Authorized.Enums
{
    public enum LoginTypeEnum
    {
        /// <summary>
        /// 账号密码
        /// </summary>
        [Description("账号密码")]
        AccountPassword = 0,

        /// <summary>
        /// 账号密码
        /// </summary>
        [Description("账号密码")]
        MPWeChat = 1
    }
}
