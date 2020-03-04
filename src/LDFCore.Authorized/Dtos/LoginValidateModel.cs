using LDFCore.Authorized.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LDFCore.Authorized.Dtos
{
    public class LoginValidateModel
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; set; }

    }
}
