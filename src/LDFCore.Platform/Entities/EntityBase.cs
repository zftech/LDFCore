using System;
using System.Collections.Generic;
using System.Text;

namespace LDFCore.Platform.Entities
{
    public class EntityBase<TKey> : Entity<TKey>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        public TKey CreatorUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastModificationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        public TKey LastModifierUserId { get; set; }
    }

    public class EntityBase : EntityBase<long>
    {

    }
}
