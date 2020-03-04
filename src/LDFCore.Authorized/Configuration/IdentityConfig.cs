using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Authorized.Configuration
{
    internal class IdentityConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResourceResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), //必须要添加，否则报无效的scope错误
                new IdentityResources.Profile()
            };
        }
    }
}
