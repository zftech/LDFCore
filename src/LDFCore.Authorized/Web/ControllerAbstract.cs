using LDFCore.FluentValidation.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Authorized.Web
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ValidateResultFormat]
    public class ControllerAbstract : ControllerBase
    {
    }
}
