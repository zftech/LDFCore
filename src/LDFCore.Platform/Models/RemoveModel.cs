using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Platform.Models
{
    public class RemoveModel<T>
    {
        /// <summary>
        /// 编号
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// 编号  , 分开
        /// </summary>
        public string Ids { get; set; }
    }
}
