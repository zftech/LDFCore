using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Platform.Result
{
    public interface IResultModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonIgnore]
        bool Successful { get; }
    }

    /// <summary>
    /// 返回结果模型泛型接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResultModel<T> : IResultModel
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        T Data { get; }
    }
}
