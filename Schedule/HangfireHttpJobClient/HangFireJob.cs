
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：HangFireJob
// 文件功能描述：HangFire任务封装
//
// 创建者：陈越锋
// 创建时间：2020年12月17日14:35:19
// 
//----------------------------------------------------------------*/
using Hangfire.HttpJob.Client;
using HangfireHttpJobClient.Config;
using HangfireHttpJobClient.Models.Dto;
using System;
using System.Threading.Tasks;

namespace HangfireHttpJobClient
{
	public static class HangFireJob
	{
		#region 周期任务 Recurring

		/// <summary>
		/// 添加周期任务
		/// </summary>
		/// <param name="request"></param>
		public static HangfirJobResult AddRecurringJob(RecurringJobRequest request)
		{
			return HangfireJobClient.AddRecurringJob(JobConfig.ServerUrl, new RecurringJob()
			{
				Cron = request.Cron,
				JobName = request.JobEnglishName,
				Method = request.HttpMethod.ToString(),
				Url = $"{JobConfig.DomainName}{request.Api}",
				Timeout = request.Timeout,
				EnableRetry = request.EnableRetry,
				ContentType = request.ContentType,
				RetryDelaysInSeconds = string.Join(',', request.RetryDelaysInSeconds),
				RetryTimes = request.RetryTimes,
				QueueName = request.QueueName,
			}, new HangfireServerPostOption()
			{
				BasicUserName = JobConfig.BasicUserName,
				BasicPassword = JobConfig.BasicUserPwd
			});
		}

		/// <summary>
		/// 移除周期任务
		/// </summary>
		/// <param name="jobName">任务名称 使用英文</param>
		public static HangfirJobResult RemoveRecurringJob(string jobName)
		{
			return HangfireJobClient.RemoveRecurringJob(JobConfig.ServerUrl, jobName, new HangfireServerPostOption()
			{
				BasicUserName = JobConfig.BasicUserName,
				BasicPassword = JobConfig.BasicUserPwd
			});
		}

		/// <summary>
		/// 添加周期任务
		/// </summary>
		/// <param name="request"></param>
		public static async Task<HangfirJobResult> AddRecurringJobAsync(RecurringJobRequest request)
		{
			return await HangfireJobClient.AddRecurringJobAsync(JobConfig.ServerUrl, new RecurringJob()
			{
				Cron = request.Cron,
				JobName = request.JobEnglishName,
				Method = request.HttpMethod.ToString(),
				Url = $"{JobConfig.DomainName}{request.Api}",
				Timeout = request.Timeout,
				EnableRetry = request.EnableRetry,
				ContentType = request.ContentType,
				RetryDelaysInSeconds = string.Join(',', request.RetryDelaysInSeconds),
				RetryTimes = request.RetryTimes
			}, new HangfireServerPostOption()
			{
				BasicUserName = JobConfig.BasicUserName,
				BasicPassword = JobConfig.BasicUserPwd
			});
		}

		/// <summary>
		/// 移除周期任务
		/// </summary>
		/// <param name="jobName">任务名称 使用英文</param>
		public static async Task<HangfirJobResult> RemoveRecurringJobAsync(string jobName)
		{
			return await HangfireJobClient.RemoveRecurringJobAsync(JobConfig.ServerUrl, jobName, new HangfireServerPostOption()
			{
				BasicUserName = JobConfig.BasicUserName,
				BasicPassword = JobConfig.BasicUserPwd
			});
		}

		#endregion

		#region 后台任务 Background

		/// <summary>
		/// 添加后台任务
		/// </summary>
		/// <param name="request"></param>
		public static AddBackgroundHangfirJobResult AddBackground(BackgroudJobRequest request)
		{
			return HangfireJobClient.AddBackgroundJob(JobConfig.ServerUrl, new BackgroundJob()
			{
				JobName = request.JobName,
				Method = request.HttpMethod.ToString(),
				Url = $"{JobConfig.DomainName}{request.Api}",
				Timeout = request.Timeout,
				DelayFromMinutes = (int)request.DelayMinutes.TotalMinutes,//延迟分钟数值
				Data = request.Data
			}, new HangfireServerPostOption()
			{
				BasicUserName = JobConfig.BasicUserName,
				BasicPassword = JobConfig.BasicUserPwd
			});
		}

		/// <summary>
		/// 移除后台任务 
		/// </summary>
		/// <param name="jobId">任务Id</param>
		public static HangfirJobResult RemoveBackground(string jobId)
		{
			return HangfireJobClient.RemoveBackgroundJob(JobConfig.ServerUrl, jobId, new HangfireServerPostOption
			{
				BasicUserName = JobConfig.BasicUserName,
				BasicPassword = JobConfig.BasicUserPwd
			});
		}

		/// <summary>
		/// 添加后台任务
		/// </summary>
		/// <param name="request"></param>
		public static async Task<AddBackgroundHangfirJobResult> AddBackgroundAsync(BackgroudJobRequest request)
		{
			return await HangfireJobClient.AddBackgroundJobAsync(JobConfig.ServerUrl, new BackgroundJob()
			{
				JobName = request.JobName,
				Method = request.HttpMethod.ToString(),
				Url = $"{JobConfig.DomainName}{request.Api}",
				Timeout = request.Timeout,
				DelayFromMinutes = (int)request.DelayMinutes.TotalMinutes,//延迟分钟数值
				Data = request.Data
			}, new HangfireServerPostOption()
			{
				BasicUserName = JobConfig.BasicUserName,
				BasicPassword = JobConfig.BasicUserPwd
			});
		}

		/// <summary>
		/// 移除后台任务 
		/// </summary>
		/// <param name="jobId">任务Id</param>
		public static async Task<HangfirJobResult> RemoveBackgroundAsync(string jobId)
		{
			return await HangfireJobClient.RemoveBackgroundJobAsync(JobConfig.ServerUrl, jobId, new HangfireServerPostOption
			{
				BasicUserName = JobConfig.BasicUserName,
				BasicPassword = JobConfig.BasicUserPwd
			});
		}

		#endregion
	}
}
