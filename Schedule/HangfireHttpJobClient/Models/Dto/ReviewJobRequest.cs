using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireHttpJobClient.Models.Dto
{
   public class ReviewJobRequest
    {
        /// <summary>
        /// 子订单id
        /// </summary>
        public long OrderItemId { get; set; }
    }
}
