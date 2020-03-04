using LDFCore.Authorized.Configuration;
using LDFCore.Authorized.Dtos;
using LDFCore.Authorized.Enums;
using LDFCore.Platform.Result;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LDFCore.Authorized.IdentityServer
{
    public class AuthServices : IAuthServices
    {

        public AuthServices(IOptions<IdsAuthentication> idsAuthentication)
        {
            _idsAuthentication = idsAuthentication.Value;
        }

        private readonly IdsAuthentication _idsAuthentication;


        /// <summary>
        /// 登录验证请求
        /// </summary>
        /// <param name="loginValidateModel"></param>
        /// <returns></returns>
        public async Task<ResultModel<AuthResut<T>>> LoginAuthRequestAsync<T>(LoginValidateModel loginValidateModel) where T : class
        {
            var result = new ResultModel<AuthResut<T>>();

            if (loginValidateModel == null || string.IsNullOrWhiteSpace(loginValidateModel.UserName))
            {
                return result.Failed("账号有误");
            }

            if (_idsAuthentication == null)
            {
                return result.Failed("未配置或注入IdsAuthentication配置");
            }

            if (string.IsNullOrWhiteSpace(_idsAuthentication.AuthorityUrl))
            {
                return result.Failed("请配置授权地址");
            }

            if (string.IsNullOrWhiteSpace(loginValidateModel.ClientId))
            {
                return result.Failed("未知请求客户端");
            }

            var authorityUrl = "";
            if (_idsAuthentication.RequireHttpsMetadata)
            {
                authorityUrl = "https://" + _idsAuthentication.AuthorityUrl;
            }
            else
            {
                authorityUrl = "http://" + _idsAuthentication.AuthorityUrl;
            }

            var client = new RestClient(authorityUrl);
            var request = new RestRequest("connect/token", Method.POST);
            request.AddParameter("client_id", loginValidateModel.ClientId);
            request.AddParameter("client_secret", "secret");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", loginValidateModel.UserName);
            request.AddParameter("password", loginValidateModel.Password);
            var resultData = (await client.ExecuteAsync<AuthResut<T>>(request)).Data;

            if (!string.IsNullOrEmpty(resultData.refresh_token))
            {
                return result.Success(resultData);
            }
            else
            {
                return result.Failed(resultData.error_description);
            }
        }
    }
}
