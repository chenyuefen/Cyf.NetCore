
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：HangFireExtent
// 文件功能描述：添加HangFire周期性任务
//
// 创建者：陈越锋
// 创建时间：2020年12月17日14:35:19
// 
//----------------------------------------------------------------*/

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireHttpJobClient
{
	public static class HangFireExtent
	{
		public static void AddHangFire(this IServiceCollection services)
		{
			//定时执行商家公告栏状态设置调整，在设置时间范围内为开启，否则为关闭 add by hujs 2020/11/26
			StoreRecurringJob.AutoUpdateStoreNoticeStateJob();
			//定时执行商品标签状态设置调整，在设置时间范围内为开启，否则为关闭 add by hujs 2020/11/27
			ProductRecurringJob.AutoUpdateLabeState();
			ProductRecurringJob.AutoUpdateWxBroadcastInfo();
			//ProductRecurringJob.PullRoomVideo();
			OrderRecurringJob.AutoConfirmJob();
			OrderRecurringJob.AutoSignJob();
		}
	}
}
