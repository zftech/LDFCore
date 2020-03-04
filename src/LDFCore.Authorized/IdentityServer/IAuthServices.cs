using LDFCore.Authorized.Dtos;
using LDFCore.Platform.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LDFCore.Authorized.IdentityServer
{
    public interface IAuthServices
    {
        /// <summary>
        /// 登录验证请求
        /// </summary>
        /// <param name="loginValidateModel"></param>
        /// <returns></returns>
        Task<ResultModel<AuthResut<T>>> LoginAuthRequestAsync<T>(LoginValidateModel loginValidateModel) where T:class;
    }
}
