﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Platform.Models.Query
{
    /// <summary>
    /// 分页
    /// </summary>
    public interface IPagerBase
    {
        /// <summary>
        /// 页数，即第几页，从1开始
        /// </summary>
        int Page { get; set; }
        /// <summary>
        /// 每页显示行数
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        long TotalCount { get; set; }
    }
}
