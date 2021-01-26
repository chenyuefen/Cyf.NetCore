/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：OrderRecurringJob
// 文件功能描述： 订单 周期性任务
//
// 创建者：陈越锋
// 创建时间：2020年12月18日14:35:19
// 
//----------------------------------------------------------------*/
using HangfireHttpJobClient.Models.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HangfireHttpJobClient
{
	public class OrderRecurringJob
	{

		/// <summary>
		/// 订单自动签收
		/// </summary>
		public static void AutoSignJob()
		{
			HangFireJob.AddRecurringJob(new RecurringJobRequest()
			{
				Api = "api/order/planJob/autosignjobs",
				HttpMethod = HttpMethod.Post,
				Cron = "0 0 2 * * ?", //每天凌晨2点执行
				JobEnglishName = "AutoSign",
				Timeout = 60 * 5 * 1000,//5分钟过期
			});
		}

		/// <summary>
		/// 订单自动收货
		/// </summary>
		public static void AutoConfirmJob()
		{
			HangFireJob.AddRecurringJob(new RecurringJobRequest()
			{
				Api = "api/order/planJob/autoconfirmjobs",
				HttpMethod = HttpMethod.Post,
				Cron = "0 0 1 20 * ?", //每月20号凌晨1点执行
				JobEnglishName = "AutoConfirm",
				EnableRetry = false,
				Timeout = 60 * 10 * 1000,//10分钟过期
			});
		}
	}
}
