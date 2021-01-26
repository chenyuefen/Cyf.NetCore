/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：VodApi
// 文件功能描述： 点播api
//
// 创建者：冼晓松
// 创建时间：2020-03-17 09:30
// 
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using TM.Infrastructure.TencentCloud.Center;
using TM.Infrastructure.TencentCloud.Config;
using TM.Infrastructure.TencentCloud.Entities.Base;
using TM.Infrastructure.TencentCloud.Entities.Vod.Common;
using TM.Infrastructure.TencentCloud.Entities.Vod.Response;
using TM.Infrastructure.TencentCloud.Module;

namespace TM.Infrastructure.TencentCloud.Api
{
    /// <summary>
    /// 点播api
    /// </summary>
    public static class VodApi
    {

        #region ----获取视频信息----

        /// <summary>
        /// 获取视频信息
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public static VideoInfonJsonResult GetVideoInfo(string fileId)
        {
            SortedDictionary<string, object> config = new SortedDictionary<string, object>(StringComparer.Ordinal);
            config["SecretId"] = CosConfig.SECRET_ID;
            config["SecretKey"] = CosConfig.SECRET_KEY;
            config["RequestMethod"] = "GET";
            config["DefaultRegion"] = "gz";
            TencentCloudApiCenter module = new TencentCloudApiCenter(new Vod(), config);
            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
            requestParams["fileId"] = fileId;
            //requestParams["infoFilter.0"] = "basicInfo";
            //requestParams["infoFilter.1"] = "transcodeInfo";                
            module.GenerateUrl("DescribeInstances", requestParams);  //https://cloud.tencent.com/document/api/213/6976
            return module.Call<VideoInfonJsonResult>("GetVideoInfo", requestParams);
        }

        #endregion

        #region ----删除视频----

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static WxJsonResult DeleteVodFile(string fileId)
        {
            SortedDictionary<string, object> config = new SortedDictionary<string, object>(StringComparer.Ordinal);
            config["SecretId"] = CosConfig.SECRET_ID;
            config["SecretKey"] = CosConfig.SECRET_KEY;
            config["RequestMethod"] = "GET";
            config["DefaultRegion"] = "gz";
            TencentCloudApiCenter module = new TencentCloudApiCenter(new Vod(), config);
            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
            requestParams["fileId"] = fileId;
            requestParams["isFlushCdn"] = 1;
            requestParams["priority"] = 1;
            module.GenerateUrl("DescribeInstances", requestParams);  //https://cloud.tencent.com/document/api/213/6976
            return module.Call<WxJsonResult>("DeleteVodFile", requestParams);
        }

        #endregion

        #region ----视频转码----

        /// <summary>
        /// 视频转码
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public static ConvertFileJsonResult ConvertVodFile(string fileId)
        {
            SortedDictionary<string, object> config = new SortedDictionary<string, object>(StringComparer.Ordinal);
            config["SecretId"] = CosConfig.SECRET_ID;
            config["SecretKey"] = CosConfig.SECRET_KEY;
            config["RequestMethod"] = "GET";
            config["DefaultRegion"] = "gz";
            TencentCloudApiCenter module = new TencentCloudApiCenter(new Vod(), config);
            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
            requestParams["fileId"] = fileId;
            requestParams["isScreenshot"] = "0";
            requestParams["isWatermark"] = "0";
            return module.Call<ConvertFileJsonResult>("ConvertVodFile", requestParams);
        }

        #endregion

        #region ----查询转码模板列表----

        /// <summary>
        /// 查询转码模板列表
        /// </summary>
        public static TranscodeTemplateListJsonResult QueryTranscodeTemplateList()
        {
            SortedDictionary<string, object> config = new SortedDictionary<string, object>(StringComparer.Ordinal);
            config["SecretId"] = CosConfig.SECRET_ID;
            config["SecretKey"] = CosConfig.SECRET_KEY;
            config["RequestMethod"] = "GET";
            config["DefaultRegion"] = "gz";
            TencentCloudApiCenter module = new TencentCloudApiCenter(new Vod(), config);
            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
            return module.Call<TranscodeTemplateListJsonResult>("QueryTranscodeTemplateList", requestParams);
        }

        #endregion

        #region ----创建转码模板----

        /// <summary>
        /// 创建转码模板
        /// </summary>
        /// <param name="name"></param>
        /// <param name="container"></param>
        /// <param name="comment"></param>
        /// <param name="isFiltrateVideo"></param>
        /// <param name="isFiltrateAudio"></param>
        /// <param name="videoInfo"></param>
        /// <param name="audioInfo"></param>
        /// <returns></returns>
        public static CreateTemplateJsonResult CreateTranscodeTemplate(string name, string container, string comment, int isFiltrateVideo, int isFiltrateAudio, VideoInfo videoInfo, AudioInfo audioInfo)
        {
            SortedDictionary<string, object> config = new SortedDictionary<string, object>(StringComparer.Ordinal);
            config["SecretId"] = CosConfig.SECRET_ID;
            config["SecretKey"] = CosConfig.SECRET_KEY;
            config["RequestMethod"] = "GET";
            config["DefaultRegion"] = "gz";
            TencentCloudApiCenter module = new TencentCloudApiCenter(new Vod(), config);
            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
            requestParams["name"] = name;
            requestParams["container"] = container;
            requestParams["comment"] = comment;
            requestParams["isFiltrateVideo"] = isFiltrateVideo;
            requestParams["isFiltrateAudio"] = isFiltrateAudio;
            requestParams["video"] = videoInfo;
            requestParams["audio"] = audioInfo;
            return module.Call<CreateTemplateJsonResult>("CreateTranscodeTemplate", requestParams);
        }

        #endregion

        #region ----查询转码模板----

        /// <summary>
        /// 查询转码模板
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public static TranscodeTemplateJsonResult QueryTranscodeTemplate(int definition)
        {
            SortedDictionary<string, object> config = new SortedDictionary<string, object>(StringComparer.Ordinal);
            config["SecretId"] = CosConfig.SECRET_ID;
            config["SecretKey"] = CosConfig.SECRET_KEY;
            config["RequestMethod"] = "GET";
            config["DefaultRegion"] = "gz";
            TencentCloudApiCenter module = new TencentCloudApiCenter(new Vod(), config);
            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
            requestParams["definition"] = definition;
            return module.Call<TranscodeTemplateJsonResult>("QueryTranscodeTemplate", requestParams);
        }

        #endregion

        #region ----更新转码模板----

        /// <summary>
        /// 更新转码模板
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="name"></param>
        /// <param name="container"></param>
        /// <param name="comment"></param>
        /// <param name="isFiltrateVideo"></param>
        /// <param name="isFiltrateAudio"></param>
        /// <param name="videoInfo"></param>
        /// <param name="audioInfo"></param>
        /// <returns></returns>
        public static WxJsonResult UpdateTranscodeTemplate(int definition, string name, string container, string comment, int isFiltrateVideo, int isFiltrateAudio, VideoInfo videoInfo, AudioInfo audioInfo)
        {
            SortedDictionary<string, object> config = new SortedDictionary<string, object>(StringComparer.Ordinal);
            config["SecretId"] = CosConfig.SECRET_ID;
            config["SecretKey"] = CosConfig.SECRET_KEY;
            config["RequestMethod"] = "GET";
            config["DefaultRegion"] = "gz";
            TencentCloudApiCenter module = new TencentCloudApiCenter(new Vod(), config);
            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
            requestParams["definition"] = definition;
            return module.Call<WxJsonResult>("UpdateTranscodeTemplate", requestParams);
        }

        #endregion

        #region ----删除转码模板----

        /// <summary>
        /// 删除转码模板
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public static WxJsonResult DeleteTranscodeTemplate(int definition)
        {
            SortedDictionary<string, object> config = new SortedDictionary<string, object>(StringComparer.Ordinal);
            config["SecretId"] = CosConfig.SECRET_ID;
            config["SecretKey"] = CosConfig.SECRET_KEY;
            config["RequestMethod"] = "GET";
            config["DefaultRegion"] = "gz";
            TencentCloudApiCenter module = new TencentCloudApiCenter(new Vod(), config);
            SortedDictionary<string, object> requestParams = new SortedDictionary<string, object>(StringComparer.Ordinal);
            requestParams["definition"] = definition;
            return module.Call<WxJsonResult>("DeleteTranscodeTemplate", requestParams);
        }

        #endregion
    }
}
