namespace TM.Core.Data.Sharding.Rules
{
    /// <summary>
    /// 分片规则接口
    /// </summary>
    public interface IShardingRule
    {
        /// <summary>
        /// 找表方法
        /// </summary>
        /// <param name="ob">实体对象</param>
        /// <returns></returns>
        string FindTable(object ob);
    }
}
