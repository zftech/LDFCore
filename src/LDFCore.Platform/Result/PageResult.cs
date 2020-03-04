using LDFCore.Platform.Models.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Platform.Result
{
    public class PageResult
    {
        public static IResultModel PageList<T>(List<T> list, IPagerBase pagerBase)
        {
            var Page = new { list,pagerBase.Page, pagerBase.PageSize,pagerBase.TotalCount };
            return ResultModel.Success(Page);
        }
    }
}
