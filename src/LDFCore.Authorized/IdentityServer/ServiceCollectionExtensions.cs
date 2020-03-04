using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using LDFCore.Authorized.Configuration;
using LDFCore.Authorized.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Authorized.IdentityServer
{
    /// <summary>
    /// 添加IdentityServer认证
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加IdentityServer认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="environmentName">环境名称</param>
        public static IServiceCollection AddIdentityServer<T>(this IServiceCollection services, IConfigurationSection section) where T : class, IResourceOwnerPasswordValidator
        {
            var apiResources = new List<ApiResource>();
            var clients = new List<Client>();

            if (section.Exists())
            {

                var idsOptions = section.Get<IdsOptions>();

                if (idsOptions != null)
                {
                    foreach (var item in idsOptions.IdsApiResources)
                    {
                        apiResources.Add(new ApiResource(item.Name, item.DisplayName));
                    }

                    foreach (var item in idsOptions.IdsClients)
                    {
                        var allowedScopes = new List<string>() { IdentityServerConstants.StandardScopes.OfflineAccess };

                        foreach (var i in item.AllowedScopes)
                        {
                            allowedScopes.Add(i);
                        }

                        clients.Add(
                        new Client
                        {
                            ClientId = item.ClientId,
                            AllowAccessTokensViaBrowser = true,
                            ClientSecrets = new[] { new Secret("secret".Sha256()) },
                            AllowedGrantTypes = GetAllowedGrantTypes(item.GrantTypes),
                            AllowedScopes = allowedScopes,
                            AllowOfflineAccess = true,
                            AccessTokenLifetime = item.AccessTokenLifetime,
                            RefreshTokenExpiration = TokenExpiration.Sliding,
                            RefreshTokenUsage = TokenUsage.ReUse,
                            UpdateAccessTokenClaimsOnRefresh = false
                        });
                    }
                }
            }

            services.AddIdentityServer()
               .AddDeveloperSigningCredential()
               .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResourceResources())
               .AddInMemoryApiResources(apiResources)
               .AddInMemoryClients(clients)
               .AddResourceOwnerValidator<T>()
               .AddProfileService<ProfileService>();
            services.TryAddSingleton<ILoginInfo, LoginInfo>();
            return services;
        }


        /// <summary>
        /// 未能匹配上默认为ResourceOwnerPasswordAndClientCredentials
        /// </summary>
        /// <param name="grantTypes">授权类型</param>
        /// <returns></returns>
        private static ICollection<string> GetAllowedGrantTypes(string grantTypes)
        {
            if (!string.IsNullOrWhiteSpace(grantTypes))
            {
                grantTypes = grantTypes.ToLower();
            }

            switch (grantTypes)
            {
                case "implicit":
                    return GrantTypes.Implicit;
                case "implicitandclientcredentials":
                    return GrantTypes.ImplicitAndClientCredentials;
                case "code":
                    return GrantTypes.Code;
                case "codeandclientcredentials":
                    return GrantTypes.CodeAndClientCredentials;
                case "hybrid":
                    return GrantTypes.Hybrid;
                case "hybridandclientcredentials":
                    return GrantTypes.HybridAndClientCredentials;
                case "clientcredentials":
                    return GrantTypes.ClientCredentials;
                case "resourceownerpassword":
                    return GrantTypes.ResourceOwnerPassword;
                case "resourceownerpasswordandclientcredentials":
                    return GrantTypes.ResourceOwnerPasswordAndClientCredentials;
                case "deviceflow":
                    return GrantTypes.DeviceFlow;
                default:
                    return GrantTypes.ResourceOwnerPasswordAndClientCredentials;
            }
        }


        /// <summary>
        /// 添加IdentityServer身份认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityServerAuthentication(this IServiceCollection services, IConfigurationSection section)
        {
            if (section.Exists())
            {
                var idsAuthentication = section.Get<IdsAuthentication>();
                services.Configure<IdsAuthentication>(section);

                if (idsAuthentication != null)
                {
                    if (idsAuthentication.AuthorityUrl == null)
                    {
                        idsAuthentication.AuthorityUrl = "";
                    }

                    var httpType = idsAuthentication.RequireHttpsMetadata ? "https://" : "http://";
                    idsAuthentication.AuthorityUrl = httpType + idsAuthentication.AuthorityUrl;

                    services.AddAuthentication(idsAuthentication.SchemeName)
                        .AddIdentityServerAuthentication(options =>
                        {
                            options.ApiName = idsAuthentication.ApiName;
                            options.RequireHttpsMetadata = idsAuthentication.RequireHttpsMetadata;
                            options.Authority = idsAuthentication.AuthorityUrl;
                        });
                }
            }

            services.AddScoped<IAuthServices, AuthServices>();

            return services;
        }
    }
}
