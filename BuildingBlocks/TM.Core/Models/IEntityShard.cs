namespace TM.Core.Models
{
    /// <summary>
    /// https://github.com/zhouguoqing/N-Sharding
    /// </summary>
    public interface IEntityShard
    {
        /// <summary>
        /// 需要分表的类，需要实现此方法， 提供分表后缀名的获取
        /// </summary>
        /// <returns></returns>
        string GetShardName();
    }
}
