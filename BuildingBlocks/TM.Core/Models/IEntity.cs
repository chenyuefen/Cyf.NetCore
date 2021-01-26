using System;

namespace TM.Core.Models
{
    public interface IEntity<TKey> : IDbTable
       where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
        void SetId();
    }

    public interface IEntity : IEntity<long>
    {
    }
}
