using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LDFCore.FluentValidation
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加FluentValidation
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IMvcBuilder AddValidators(this IMvcBuilder builder, IServiceCollection services, string assemblies)
        {
            //注入验证结果格式化器
            services.TryAddSingleton<IValidateResultFormatHandler, ValidateResultFormatHandler>();
            builder.AddFluentValidation(fv =>
            {
                foreach (var item in assemblies.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var assembly = Assembly.Load(item);
                    fv.RegisterValidatorsFromAssembly(assembly);
                }
            });
            //当一个验证失败时，后续的验证不再执行
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            return builder;
        }
    }
}
