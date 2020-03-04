using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Authorized.Dtos
{
    public class LoginValidateResultModel<T> where T:class
    {
        public AuthResut<T> authResut { get; set; }
    }
}
