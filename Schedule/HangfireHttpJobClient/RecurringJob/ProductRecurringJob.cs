/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：ProductRecurringJob
// 文件功能描述： 商品 周期业务
//
// 创建者：陈越锋
// 创建时间：2020年12月18日14:35:19
// 
//----------------------------------------------------------------*/

using Hangfire.HttpJob.Client;
using HangfireHttpJobClient.Config;
using HangfireHttpJobClient.Models.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HangfireHttpJobClient
{

	/// <summary>
	/// 商品 周期性任务
	/// </summary>
	public static class ProductRecurringJob
	{

		#region 商品标签

		/// <summary>
		/// 定时更新商品标签状态
		/// </summary>
		public static void AutoUpdateLabeState()
		{
			HangFireJob.AddRecurringJob(new RecurringJobRequest()
			{
				Api = "api/system/modify/label/state/job",
				HttpMethod = HttpMethod.Post,
				Cron = "0 30 0 * * ?", //每天凌晨30分执行
				JobEnglishName = "AutoUpdateGoodsLabelState",
			});
		}

		#endregion

		#region 直播

		/// <summary>
		/// 定时更新直播信息
		/// </summary>
		public static void AutoUpdateWxBroadcastInfo()
		{
			HangFireJob.AddRecurringJob(new RecurringJobRequest()
			{
				Api = "api/product/job/wxbroadcast/autoupdatewxbroadcastinfo",
				HttpMethod = HttpMethod.Get,
				Cron = "0 0/10 * * * ? ", //每天每10分执行
				JobEnglishName = "AutoUpdateGoodsLabelState",
			});
		}

		/// <summary>
		/// 拉取房间视频
		/// </summary>
		public static void PullRoomVideo()
		{
			//2022年过期  暂时不用处理
			HangFireJob.AddRecurringJob(new RecurringJobRequest()
			{
				Api = "api/product/job/wxbroadcast/pullroomvideo",
				HttpMethod = HttpMethod.Get,
				Cron = "0 0 4/23 1/1 * ? *", //每天4点执行一次
				JobEnglishName = "AutoUpdateGoodsLabelState",
			});
		}

		#endregion
	}
}
