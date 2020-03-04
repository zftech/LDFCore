using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Platform.Attributes
{
    /// <summary>
    /// 单例注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class SingletonAttribute : Attribute
    {
    }
}
