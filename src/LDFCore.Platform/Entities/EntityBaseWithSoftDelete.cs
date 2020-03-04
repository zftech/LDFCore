using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Platform.Entities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class EntityBaseWithSoftDelete<TKey> : EntityBase<TKey>, IDelete
    {
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EntityBaseWithSoftDelete : EntityBaseWithSoftDelete<long>
    {

    }
}
