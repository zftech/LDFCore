using LDFCore.Platform.Models.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Platform.Query
{
    public class QueryModel:Pager
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; }
    }
}
