using LDFCore.Common.FileUpload.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LDFCore.Common.FileUpload
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 自定义文件上传
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUoloadFile(this IApplicationBuilder app)
        {
            var fileUploadOption = app.ApplicationServices.GetService<IOptionsMonitor<FileUploadOption>>().CurrentValue;
            if (!Directory.Exists(fileUploadOption.Root))
            {
                Directory.CreateDirectory(fileUploadOption.Root);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(fileUploadOption.Root),
                RequestPath = fileUploadOption.RequestPath,
                ServeUnknownFileTypes = true
            });
            app.UseWhen(context => context.Request.Path.StartsWithSegments(fileUploadOption.RequestRoute),
               builder => builder.Use(async (context, next) =>
               {
                   context.Request.EnableBuffering();
                   await next.Invoke();
               }));
            return app;
        }
    }
}
