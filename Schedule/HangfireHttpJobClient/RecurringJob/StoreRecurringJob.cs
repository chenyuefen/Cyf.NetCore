/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：StoreRecurringJob
// 文件功能描述： 商家 周期性任务
//
// 创建者：陈越锋
// 创建时间：2020年12月18日14:35:19
// 
//----------------------------------------------------------------*/

using Hangfire.HttpJob.Client;
using HangfireHttpJobClient.Config;
using HangfireHttpJobClient.Models.Dto;
using System.Net.Http;

namespace HangfireHttpJobClient
{
	public static class StoreRecurringJob
	{

		#region 商家公告栏

		/// <summary>
		/// 定时更新商家公告栏状态
		/// </summary>
		public static void AutoUpdateStoreNoticeStateJob()
		{
			HangFireJob.AddRecurringJob(new RecurringJobRequest()
			{
				Api = "api/jobserver/storeshangfire/setstorenocticestate",
				HttpMethod = HttpMethod.Post,
				Cron = "0 10 0 * * ?", //每天凌晨10分钟执行
				JobEnglishName = "AutoUpdateStoreNoticeState",
			});
		}

		#endregion

	}
}
