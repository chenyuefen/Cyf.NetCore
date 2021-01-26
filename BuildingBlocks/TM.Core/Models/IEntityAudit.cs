using System;

namespace TM.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntityAudit<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        DateTimeOffset CreatedTime { get; set; }
        DateTimeOffset UpdatedTime { get; set; }
    }

    public interface IEntityAudit : IEntityAudit<long>
    {
    }
}
