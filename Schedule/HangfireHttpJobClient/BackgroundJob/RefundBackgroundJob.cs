
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：AddRefundBackgroundJob
// 文件功能描述： 添加订单退款计划常规作业
//
// 创建者：陈子华
// 创建时间：2020年5月27日10:14:07
// 
//----------------------------------------------------------------*/


using Hangfire.HttpJob.Client;
using HangfireHttpJobClient.Config;
using HangfireHttpJobClient.Models.Dto;
using System;

namespace HangfireHttpJobClient
{
    public static class RefundBackgroundJob
    {
        #region 待商家审核超时，自动同意申请（2天）
        public static void AddApproveJob(RefundJobRequest request)
        {
            HangFireJob.AddBackground(new BackgroudJobRequest()
            {
                Api = "api/jobserver/refund/approve",
                JobName = "待审核超时",
                DelayMinutes = TimeSpan.FromDays(2),
                Data = request
            });
        }
        #endregion

        #region 商家拒绝待用户处理超时（3天）
        public static void ReApplyJob(RefundJobRequest request)
        {
            HangFireJob.AddBackground(new BackgroudJobRequest()
            {
                Api = "api/jobserver/refund/reapply",
                JobName = "商家拒绝待用户处理超时",
                DelayMinutes = TimeSpan.FromDays(3),
                Data = request
            });
        }
        #endregion

        #region 待买家发货超时处理（3天）
        public static void WaitSendJob(RefundJobRequest request)
        {
            HangFireJob.AddBackground(new BackgroudJobRequest()
            {
                Api = "api/jobserver/refund/send",
                JobName = "待买家发货处理超时",
                DelayMinutes = TimeSpan.FromDays(3),
                Data = request
            });
        }
        #endregion

        #region 发货后待商家收货超时（10天）
        public static void ReceivedJob(RefundJobRequest request)
        {
            HangFireJob.AddBackground(new BackgroudJobRequest()
            {
                Api = "api/jobserver/refund/received",
                JobName = "发货后待商家处理超时",
                DelayMinutes = TimeSpan.FromDays(10),
                Data = request
            });
        }
        #endregion

        #region 仅退款，待商家审核超时作业（4天）
        public static void ApproveNotSendJob(RefundJobRequest request)
        {
            HangFireJob.AddBackground(new BackgroudJobRequest()
            {
                Api = "api/jobserver/refund/approvenotsend",
                JobName = "仅退款待商家审核超时作业",
                DelayMinutes = TimeSpan.FromDays(4),
                Data = request
            });
        }
        #endregion
    }
}
