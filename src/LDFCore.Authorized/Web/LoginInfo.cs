using LDFCore.Authorized.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LDFCore.Authorized.Web
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginInfo : ILoginInfo
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginInfo(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// 账户编号
        /// </summary>
        public long AccountId
        {
            get
            {
                var accountId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.AccountId);
                if (accountId != null && accountId.Value!=null)
                {
                    return long.Parse(accountId.Value); 
                }
                return 0;
            }
        }

        public string AccountName
        {
            get
            {
                var accountName = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.AccountName);

                if (accountName == null || accountName.Value!=null)
                {
                    return "";
                }

                return accountName.Value;
            }
        }

        /// <summary>
        /// 请求平台
        /// </summary>
        public PlatformEnum Platform
        {
            get
            {
                var pt = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.Platform);
                if (pt != null && pt.Value != null)
                {
                    return (PlatformEnum)int.Parse(pt.Value);
                }

                return PlatformEnum.UnKnown;
            }
        }

        public AccountType AccountType
        {
            get
            {
                var ty = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.AccountType);

                if (ty != null && ty.Value!=null)
                {
                    return (AccountType)int.Parse(ty.Value);
                }

                return AccountType.UnKnown;
            }
        }

        /// <summary>
        /// 获取当前用户IP(包含IPv和IPv6)
        /// </summary>
        public string IP
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }

        /// <summary>
        /// 获取当前用户IPv4
        /// </summary>
        public string IPv4
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        /// <summary>
        /// 获取当前用户IPv6
        /// </summary>
        public string IPv6
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString();
            }
        }
    }
}
