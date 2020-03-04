using LDFCore.Swagger.Configuration;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Swagger
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 自定义Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, Options options)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{options.Version}/swagger.json", options.Title);
            });
            return app;
        }
    }
}
