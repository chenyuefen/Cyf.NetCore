using System;
using System.ComponentModel.DataAnnotations.Schema;
using TM.Infrastructure.Helpers;

namespace TM.Core.Models
{
    public class EntityBase<TKey> : IEntity<TKey>
       where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
        public virtual void SetId() { }
    }

    public abstract class EntityBase : EntityBase<long>, IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Id { get; set; }

        public override void SetId()
        {
            Id = IdGener.GetLong();
        }
    }
}
