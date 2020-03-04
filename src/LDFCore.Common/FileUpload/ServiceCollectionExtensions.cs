using LDFCore.Common.FileUpload.Configuration;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Common.FileUpload
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUpload(this IServiceCollection services, IConfiguration configurationAction)
        {
            //设置接收文件长度的最大值。
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });
            services.Configure<FileUploadOption>(configurationAction);
            return services;
        }
    }
}
