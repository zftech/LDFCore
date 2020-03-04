using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Authorized.Dtos
{
    public class AuthResut<T> where T:class
    {
        /// <summary>
        /// 错误
        /// </summary>
        public string error { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// token类型
        /// </summary>
        public string token_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string error_description { get; set; }

        public T user { get; set; }
    }
}
