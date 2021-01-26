using Hangfire.HttpJob.Client;
using HangfireHttpJobClient.Config;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HangfireHttpJobClient.Models.Dto
{
	/// <summary>
	/// 周期性任务请求
	/// </summary>
	public class RecurringJobRequest
	{
		/// <summary>
		/// 时间Cron表达式
		/// </summary>
		public string Cron { get; set; }

		/// <summary>
		/// 任务名称 使用英文
		/// </summary>
		public string JobEnglishName { get; set; }

		/// <summary>
		/// 请求Api
		/// </summary>
		public string Api { get; set; }

		/// <summary>
		/// 消息队列名称
		/// </summary>
		public string QueueName { get; set; } = "recurring";

		/// <summary>
		/// 超时时间 单位:毫秒
		/// </summary>
		public int Timeout { get; set; } = 5000;

		/// <summary>
		/// 请求文本类型
		/// </summary>
		public string ContentType { get; set; } = "application/json";

		/// <summary>
		/// 请求方式 Get | Post
		/// </summary>
		public HttpMethod HttpMethod { get; set; } = HttpMethod.Post;

		/// <summary>
		/// 是否支持重试
		/// </summary>
		public bool EnableRetry { get; set; } = true;

		/// <summary>
		/// 重试延时时间 单位:秒
		/// </summary>
		public List<int> RetryDelaysInSeconds { get; set; } = new List<int>() { 20, 30, 60 };

		/// <summary>
		/// 重试次数
		/// </summary>
		public int RetryTimes { get; set; } = 3;
	}

	/// <summary>
	/// 后台任务请求
	/// </summary>
	public class BackgroudJobRequest
	{

		/// <summary>
		/// 任务名称
		/// </summary>
		public string JobName { get; set; }

		/// <summary>
		/// 请求Api
		/// </summary>
		public string Api { get; set; }

		/// <summary>
		/// 超时时间 单位:毫秒
		/// </summary>
		public int Timeout { get; set; } = 5000;

		/// <summary>
		/// 请求方式
		/// </summary>
		public HttpMethod HttpMethod { get; set; } = HttpMethod.Post;

		/// <summary>
		/// 延迟分钟数后执行
		/// </summary>
		public TimeSpan DelayMinutes { get; set; }

		/// <summary>
		/// 请求数据
		/// </summary>
		public object Data { get; set; }

	}
}
