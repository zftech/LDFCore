using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Platform.Models
{
    /// <summary>
    /// 可选项返回模型
    /// </summary>
    public class OptionResultModel<T>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public object Data { get; set; }
    }
}
