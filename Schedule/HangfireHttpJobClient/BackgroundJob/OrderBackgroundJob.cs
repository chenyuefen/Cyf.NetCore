/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：OrderBackgroundJob
// 文件功能描述： 添加订单相关计划常规作业
//
// 创建者：陈越锋
// 创建时间：2020年12月18日14:35:19
// 
//----------------------------------------------------------------*/


using Hangfire;
using Hangfire.HttpJob.Client;
using HangfireHttpJobClient.Config;
using HangfireHttpJobClient.Models.Dto;
using System;
using System.Net.Http;

namespace HangfireHttpJobClient
{
	public static class OrderBackgroundJob
	{

		#region 物流自动签收后7天自动确认收货(7天)

		public static void AddAutoConfirmBgJob(OrderTimeJobRequest dto)
		{
			HangFireJob.AddBackground(new BackgroudJobRequest()
			{
				Api = "api/order/job/autoconfirmjob",
				JobName = "自动确认收货",
				DelayMinutes = TimeSpan.FromDays(7),
				Data = dto
			});
		}

		#endregion

		#region 确认收货后7天，结算订单金额(7天)

		public static void SettleAccountsBgJob(OrderTimeJobRequest dto)
		{
			HangFireJob.AddBackground(new BackgroudJobRequest()
			{
				Api = "api/order/job/settleaccountsjob",
				JobName = "结算订单金额",
				DelayMinutes = TimeSpan.FromDays(7),
				Data = dto
			});
		}

		#endregion

		#region 订单超时未支付关闭(1小时)

		public static void OrderCloseTimeJob(OrderCloseTimeJobRequest dto)
		{
			HangFireJob.AddBackground(new BackgroudJobRequest()
			{
				Api = "api/order/planJob/orderclose",
				JobName = "订单未支付超时关闭",
				DelayMinutes = TimeSpan.FromMinutes(dto.DelayFromMinutes),
				Data = dto
			});
		}

		/// <summary>
		/// 即将到期
		/// </summary>
		public static void OrderAboutExpireJob(OrderCloseTimeJobRequest dto)
		{
			HangFireJob.AddBackground(new BackgroudJobRequest()
			{
				Api = "api/order/planJob/orderaboutexpire",
				JobName = "订单即将到期",
				DelayMinutes = TimeSpan.FromMinutes(dto.DelayFromMinutes),
				Data = dto
			});
		}

		#endregion

		#region 确认收货15天后自动评价(15天)

		/// <summary>
		/// 确认收货15天后自动评价
		/// </summary>
		/// <param name="dto"></param>
		public static void AutoReviewJob(ReviewJobRequest dto)
		{
			HangFireJob.AddBackground(new BackgroudJobRequest()
			{
				Api = "api/order/job/autoreviewjo",
				JobName = "自动评论",
				DelayMinutes = TimeSpan.FromDays(15),
				Data = dto
			});
		}

		#endregion

		#region 虚拟商品订单超时未支付关闭(1小时)

		public static void VirtualOrderCloseTimeJob(OrderCloseTimeJobRequest dto)
		{
			HangFireJob.AddBackground(new BackgroudJobRequest()
			{
				Api = "api/order/Job/virtualorderclose",
				JobName = "虚拟订单未支付超时关闭",
				DelayMinutes = TimeSpan.FromMinutes(60),
				Data = dto
			});
		}

		#endregion

		#region 预售活动结束3分钟后自动分派订单给商家(3分钟)

		public static AddBackgroundHangfirJobResult AttrebutePresaleJob(ActivityJobRequest activityJobRequest)
		{
			var result = HangFireJob.AddBackground(new BackgroudJobRequest()
			{
				Api = "api/order/job/autoattrebutepresale",
				JobName = "自动分派订单给供应商",
				DelayMinutes = TimeSpan.FromMinutes(activityJobRequest.DelayFromMinutes),
				Data = activityJobRequest
			});
			return result;
		}

		#endregion

	}
}