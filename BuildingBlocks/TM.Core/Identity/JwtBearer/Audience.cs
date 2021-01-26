namespace TM.Core.Identity.JwtBearer
{
    public class Audience
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }
        /// <summary>
        /// 枚举 AppType
        /// </summary>
        public string AppType { get; set; }
        /// <summary>
        /// 受委派方
        /// </summary>
        public string Assignee { get; set; }
        /// <summary>
        /// 状态 0禁用  1启用
        /// </summary>
        public int Status { get; set; }
    }
}
