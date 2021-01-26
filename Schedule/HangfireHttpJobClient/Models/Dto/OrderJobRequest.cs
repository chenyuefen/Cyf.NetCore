namespace HangfireHttpJobClient.Models.Dto
{
    public class OrderJobRequest
    {

    }

    #region 订单的常规后台计划作业请求

    public class OrderTimeJobRequest
    {
        public long ItemId { get; set; }
    }

    #endregion

    #region 订单超时未支付关闭(30分钟)

    public class OrderCloseTimeJobRequest
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 延迟分钟
        /// </summary>
        public int DelayFromMinutes { get; set; }
    }

    #endregion
}