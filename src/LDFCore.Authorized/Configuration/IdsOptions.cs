using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Authorized.Configuration
{
    public class IdsOptions
    {
        /// <summary>
        /// api
        /// </summary>
        public List<IdsApiResource> IdsApiResources { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        public List<IdsClient> IdsClients { get; set; }

    }

    public class IdsApiResource
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; }

    }

    public class IdsClient
    {
        /// <summary>
        /// 客户端编号
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// token有效期
        /// </summary>
        public int AccessTokenLifetime { get; set; }

        /// <summary>
        /// 允许的范围
        /// </summary>
        public string[] AllowedScopes { get; set; }

        /// <summary>
        /// 授权类型
        /// </summary>
        public string GrantTypes { get; set; }
    }

    public class IdsAuthentication
    {
        /// <summary>
        /// 身份验证方案名称
        /// </summary>
        public string SchemeName { get; set; }

        /// <summary>
        /// 验证API名称
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// 是否需要https
        /// </summary>
        public bool RequireHttpsMetadata { get; set; }

        /// <summary>
        /// 授权地址
        /// </summary>
        public string AuthorityUrl { get; set; }
    }

}
