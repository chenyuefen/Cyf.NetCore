/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：TencentCloudApiCenter
// 文件功能描述： 腾讯公共模块调用类
//
// 创建者：冼晓松
// 创建时间：2020-03-17 09:30
// 
//----------------------------------------------------------------*/

using System.Collections.Generic;
using TM.Infrastructure.Json;
using TM.Infrastructure.TencentCloud.Entities.Base;
using TM.Infrastructure.TencentCloud.Enums;
using TM.Infrastructure.TencentCloud.Module;


namespace TM.Infrastructure.TencentCloud.Center
{
    /// <summary>
    /// 腾讯公共模块调用类
    /// </summary>
    public class TencentCloudApiCenter
    {
        private Base module;

        /// <summary>
        /// 构造模块调用类
        /// </summary>
        /// <param name="module">实际模块实例</param>
        /// <param name="config">模块配置参数</param>
        public TencentCloudApiCenter(Base module, SortedDictionary<string, object> config)
        {
            this.module = module;
            this.module.setConfig(config);
        }

        /// <summary>
        /// 生成Api调用地址
        /// </summary>
        /// <param name="actionName">模块动作名称</param>
        /// <param name="requestParams">模块请求参数</param>
        /// <returns>Api调用地址</returns>
        public string GenerateUrl(string actionName, SortedDictionary<string, object> requestParams)
        {
            return module.GenerateUrl(actionName, requestParams);
        }

        /// <summary>
        /// 公共的Api调用
        /// </summary>
        /// <param name="actionName">模块动作名称</param>
        /// <param name="requestParams">模块请求参数</param>
        /// <returns>json字符串</returns>
        public T Call<T>(string actionName, SortedDictionary<string, object> requestParams)
        {
            var m = module.GetType();
            var method = m.GetMethod(actionName);
            if (method != null)
            {
                object[] p = { requestParams };
                string errorText = (string)method.Invoke(module, p);
                return GetResult<T>(errorText);
            }
            string returnText = module.Call(actionName, requestParams);
            return GetResult<T>(returnText);
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string SecretId
        {
            set { module.SecretId = value; }
        }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey
        {
            set { module.SecretKey = value; }
        }

        /// <summary>
        /// 默认区
        /// </summary>
        public string DefaultRegion
        {
            set { module.DefaultRegion = value; }
        }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string RequestMethod
        {
            set { module.RequestMethod = value; }
        }

        /// <summary>
        /// 获取Post结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnText"></param>
        /// <returns></returns>
        public static T GetResult<T>(string returnText)
        {
            if (returnText.Contains("code"))
            {
                WxJsonResult errorResult = returnText.ToObject<WxJsonResult>();
                if (errorResult.code != ReturnCode.Success)
                {
                    //wait TODO：
                    //发生错误                   
                }
            }
            return returnText.ToObject<T>();
        }

    }
}
