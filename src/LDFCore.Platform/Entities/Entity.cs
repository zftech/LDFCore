using LDFCore.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;

namespace LDFCore.Platform.Entities
{
    /// <summary>
    /// 包含指定类型主键的实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class Entity<TKey> : IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [System.ComponentModel.DataAnnotations.Key]
        [Column(IsIdentity = true, IsPrimary = true)]
        public virtual TKey Id { get; set; }
    }

    /// <summary>
    /// 主键类型为long的实体
    /// </summary>
    public class Entity : Entity<long>
    {
        
    }
}
