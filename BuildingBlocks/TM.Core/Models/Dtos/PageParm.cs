using System;

namespace TM.Core.Models.Dtos
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PageParm
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 每页总条数
        /// </summary>
        public int Limit { get; set; } = 15;

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序的字段
        /// </summary>
        public string OrderByFields { get; set; }
        /// <summary>
        /// 时间筛选字段
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        //public DateTimeOffset? FromTime { get; set; }
        public DateTime? FromTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        //public DateTimeOffset? ToTime { get; set; }
        public DateTime? ToTime { get; set; }
        /// <summary>
        /// 0顺序，1倒序
        /// </summary>
        public int? Sort { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
