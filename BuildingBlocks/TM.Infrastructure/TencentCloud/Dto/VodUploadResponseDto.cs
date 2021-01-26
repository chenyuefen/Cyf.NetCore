using System.Collections.Generic;

namespace TM.Infrastructure.TencentCloud.Dto
{
    public class VodUploadResponseDto
    {
    }

    #region Vod 服务端上传响应类

    public class VodUploadResponseV2
    {
        /// <summary>
        /// 媒体文件的唯一标识。
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// 媒体播放地址。
        /// </summary>
        public string MediaUrl { get; set; }

        /// <summary>
        /// 媒体封面地址
        /// </summary>
        public string CoverUrl { get; set; }

        /// <summary>
        /// 唯一请求 ID，每次请求都会返回。定位问题时需要提供该次请求的 RequestId。
        /// </summary>
        public string RequestId { get; set; }
    }

    #endregion


    #region 拉去事件通知

    public class PullEventsResponseDto
    {
        public PullEventsResponse Response { get; set; }
    }

    public class PullEventsResponse
    {
        public List<ProcedureStateChangedCallBackDto> EventSet { get; set; }
    }

    #endregion

    #region 任务流状态变更 回调数据

    public class ProcedureStateChangedCallBackDto
    {
        /// <summary>
        /// 事件句柄
        /// </summary>
        public string EventHandle { get; set; }

        /// <summary>
        /// 回调事件类型
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// 数据内容
        /// </summary>
        public ProcedureStateChangedDto ProcedureStateChangeEvent { get; set; }
    }

    public class ProcedureStateChangedDto
    {
        /// <summary>
        /// 视频处理任务 ID。
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 任务流状态，取值：
        /// PROCESSING：处理中；
        /// FINISH：已完成。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 媒体文件 ID
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// 媒体文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 媒体文件地址
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// 原始视频的元信息。
        /// </summary>
        public MediaMetaData MetaData { get; set; }

        /// <summary>
        /// 视频处理任务的执行状态与结果
        /// </summary>
        public List<MediaProcessTaskResult> MediaProcessResultSet { get; set; }

        /// <summary>
        /// 视频内容审核任务的执行状态与结果。
        /// </summary>
        public List<AiContentReviewResult> AiContentReviewResultSet { get; set; }

        /// <summary>
        /// 视频内容分析任务的执行状态与结果。
        /// </summary>
        public List<AiAnalysisResult> AiAnalysisResultSet { get; set; }
    }

    #region 原始视频的元信息

    /// <summary>
    /// 原始视频的元信息。
    /// </summary>
    public class MediaMetaData
    {
        /// <summary>
        /// 上传的媒体文件大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 视频流高度的最大值，单位：px。
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 视频流宽度的最大值，单位：px。
        /// </summary>
        public int Width { get; set; }
    }

    #endregion

    #region 视频处理任务的执行状态与结果。

    public class MediaProcessTaskResult
    {
        /// <summary>
        /// 任务的类型,可以取的值有：
        /// Transcode：转码
        /// AnimatedGraphics：转动图
        /// SnapshotByTimeOffset：时间点截图
        /// SampleSnapshot：采样截图
        /// ImageSprites：雪碧图
        /// CoverBySnapshot：截图做封面
        /// AdaptiveDynamicStreaming：自适应码流
        /// </summary>
        public string Type { get; set; }

        public MediaProcessTaskTranscodeResult TranscodeTask { get; set; }

        public MediaProcessTaskCoverBySnapshotResult CoverBySnapshotTask { get; set; }
    }

    #region 视频转码任务的查询结果，当任务类型为 Transcode 时有效。

    public class MediaProcessTaskTranscodeResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 转码任务的输出。
        /// 注意：此字段可能返回 null，表示取不到有效值。
        /// </summary>
        public MediaTranscodeItem Output { get; set; }
    }

    #region 转码后的视频文件地址

    public class MediaTranscodeItem
    {
        /// <summary>
        /// 转码后的视频文件地址。
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 转码规格 ID
        /// </summary>
        public int Definition { get; set; }
    }

    #endregion

    #endregion

    #region 对视频截图做封面任务的查询结果，当任务类型为 CoverBySnapshot 时有效。

    public class MediaProcessTaskCoverBySnapshotResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 对视频截图做封面任务的输出。
        /// </summary>
        public CoverBySnapshotTaskOutput Output { get; set; }
    }

    public class CoverBySnapshotTaskOutput
    {
        /// <summary>
        /// 封面 URL。
        /// </summary>
        public string CoverUrl { get; set; }
    }

    #endregion

    #endregion

    #region 视频内容审核任务的执行状态与结果。

    public class AiContentReviewResult
    {
        /// <summary>
        /// 任务的类型，可以取的值有：
        /// Porn：图片鉴黄
        /// Terrorism：图片鉴恐
        /// Political：图片鉴政
        /// Porn.Asr：Asr 文字鉴黄
        /// Porn.Ocr：Ocr 文字鉴黄
        /// Political.Asr：Asr 文字鉴政
        /// Political.Ocr：Ocr 文字鉴政
        /// Terrorism.Ocr：Ocr 文字鉴恐
        /// Prohibited.Asr：Asr 文字鉴违禁
        /// Prohibited.Ocr：Ocr 文字鉴违禁
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 视频内容审核智能画面鉴黄任务的查询结果
        /// </summary>
        public AiReviewTaskPornResult PornTask { get; set; }

        /// <summary>
        /// 视频内容审核智能画面鉴恐任务的查询结果
        /// </summary>
        public AiReviewTaskTerrorismResult TerrorismTask { get; set; }

        /// <summary>
        /// 视频内容审核智能画面鉴政任务的查询结果，
        /// </summary>
        public AiReviewTaskPoliticalResult PoliticalTask { get; set; }

        /// <summary>
        /// 视频内容审核 Asr 文字鉴黄任务的查询结果
        /// </summary>
        public AiReviewTaskPornAsrResult PornAsrTask { get; set; }

        /// <summary>
        /// 视频内容审核 Ocr 文字鉴黄任务的查询结果
        /// </summary>
        public AiReviewTaskPornOcrResult PornOcrTask { get; set; }

        /// <summary>
        /// 视频内容审核 Asr 文字鉴政任务的查询结果
        /// </summary>
        public AiReviewTaskPoliticalAsrResult PoliticalAsrTask { get; set; }

        /// <summary>
        /// 视频内容审核 Ocr 文字鉴政任务的查询结
        /// </summary>
        public AiReviewTaskPoliticalOcrResult PoliticalOcrTask { get; set; }

        /// <summary>
        /// 视频内容审核 Ocr 文字鉴恐任务的查询结果
        /// </summary>
        public AiReviewTaskTerrorismOcrResult TerrorismOcrTask { get; set; }

        /// <summary>
        /// 视频内容审核 Asr 文字鉴违禁任务的查询结果
        /// </summary>
        public AiReviewTaskProhibitedAsrResult ProhibitedAsrTask { get; set; }

        /// <summary>
        /// 视频内容审核 Ocr 文字鉴违禁任务的查询结果
        /// </summary>
        public AiReviewTaskProhibitedOcrResult ProhibitedOcrTask { get; set; }
    }

    #region 视频内容审核智能画面鉴黄任务的查询结果，当任务类型为 Porn 时有效。

    /// <summary>
    /// 内容审核鉴黄任务结果类型
    /// </summary>
    public class AiReviewTaskPornResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 内容审核鉴黄任务输出。
        /// </summary>
        public AiReviewPornTaskOutput Output { get; set; }
    }

    #region 鉴黄结果信息

    public class AiReviewPornTaskOutput
    {
        /// <summary>
        /// 视频鉴黄评分，分值为0到100。
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// 鉴黄结果建议，取值范围
        /// pass。
        /// review。
        /// block。
        /// </summary>
        public string Suggestion { get; set; }

        /// <summary>
        /// 视频鉴黄结果标签，取值范围：
        /// porn：色情。
        /// sexy：性感。
        /// vulgar：低俗。
        /// intimacy：亲密行为。
        /// </summary>
        public string Label { get; set; }
    }

    #endregion

    #endregion

    #region 视频内容审核智能画面鉴恐任务的查询结果，当任务类型为 Terrorism 时有效。

    public class AiReviewTaskTerrorismResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 内容审核鉴恐任务输出。
        /// </summary>
        public AiReviewTerrorismTaskOutput Output { get; set; }
    }

    #region 暴恐信息。

    public class AiReviewTerrorismTaskOutput
    {
        /// <summary>
        /// 视频暴恐评分，分值为0到100。
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// 暴恐结果建议，取值范围
        /// pass。
        /// review。
        /// block。
        /// </summary>
        public string Suggestion { get; set; }

        /// <summary>
        /// 视频暴恐结果标签，取值范围
        /// guns：武器枪支。
        /// crowd：人群聚集。
        /// police：警察部队。
        /// bloody：血腥画面。
        /// banners：暴恐旗帜。
        /// militant：武装分子。
        /// explosion：爆炸火灾。
        /// terrorists：暴恐人物。
        /// </summary>
        public string Label { get; set; }
    }

    #endregion

    #endregion

    #region 视频内容审核智能画面鉴政任务的查询结果，当任务类型为 Political 时有效

    public class AiReviewTaskPoliticalResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 内容审核鉴政任务输出。
        /// </summary>
        public AiReviewPoliticalTaskOutput Output { get; set; }
    }

    #region 涉政信息

    public class AiReviewPoliticalTaskOutput
    {
        /// <summary>
        /// 视频涉政评分，分值为0到100。
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// 涉政结果建议，取值范围
        /// pass。
        /// review。
        /// block。
        /// </summary>
        public string Suggestion { get; set; }

        /// <summary>
        /// 视频鉴政结果标签，取值范围：
        /// politician：政治人物。
        /// violation_photo：违规图标。
        /// </summary>
        public string Label { get; set; }
    }

    #endregion

    #endregion

    #region 视频内容审核 Asr 文字鉴黄任务的查询结果，当任务类型为 Porn.Asr 时有效。

    public class AiReviewTaskPornAsrResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 内容审核 Asr 文字鉴黄任务输出
        /// </summary>
        public AiReviewPornAsrTaskOutput Output { get; set; }
    }

    #region 内容审核 Asr 文字鉴黄任务输出。

    public class AiReviewPornAsrTaskOutput
    {
        /// <summary>
        /// Asr 文字涉黄评分，分值为0到100
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// Asr 文字涉黄结果建议，取值范围
        /// pass。
        /// review。
        /// block。
        /// </summary>
        public string Suggestion { get; set; }
    }

    #endregion

    #endregion

    #region 视频内容审核 Ocr 文字鉴黄任务的查询结果，当任务类型为 Porn.Ocr 时有效。

    public class AiReviewTaskPornOcrResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        public AiReviewPornOcrTaskOutput Output { get; set; }
    }

    #region 内容审核 Ocr 文字鉴黄任务输出。

    public class AiReviewPornOcrTaskOutput
    {
        /// <summary>
        /// Asr 文字涉黄评分，分值为0到100
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// Ocr 文字涉黄结果建议，取值范围
        /// pass。
        /// review。
        /// block。
        /// </summary>
        public string Suggestion { get; set; }
    }

    #endregion

    #endregion

    #region 视频内容审核 Asr 文字鉴政任务的查询结果，当任务类型为 Political.Asr 时有效。

    public class AiReviewTaskPoliticalAsrResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 内容审核 Asr 文字鉴政任务输出。
        /// </summary>
        public AiReviewPoliticalAsrTaskOutput Output { get; set; }
    }

    #region 内容审核 Asr 文字鉴政任务输出。

    public class AiReviewPoliticalAsrTaskOutput
    {
        /// <summary>
        /// Asr 文字涉黄评分，分值为0到100
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// Asr 文字涉政、敏感结果,建议取值范围
        /// pass。
        /// review。
        /// block。
        /// </summary>
        public string Suggestion { get; set; }
    }

    #endregion

    #endregion

    #region 视频内容审核 Ocr 文字鉴政任务的查询结果，当任务类型为 Political.Ocr 时有效。

    public class AiReviewTaskPoliticalOcrResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        public AiReviewPoliticalOcrTaskOutput Output { get; set; }
    }

    #region 内容审核 Ocr 文字鉴政任务输出。

    public class AiReviewPoliticalOcrTaskOutput
    {
        /// <summary>
        /// Asr 文字涉黄评分，分值为0到100
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// Ocr 文字涉政、敏感结果建议，取值范围：
        /// pass。
        /// review。
        /// block。
        /// </summary>
        public string Suggestion { get; set; }
    }

    #endregion

    #endregion

    #region 视频内容审核 Ocr 文字鉴恐任务的查询结果，当任务类型为 Terrorism.Ocr 时有效。

    public class AiReviewTaskTerrorismOcrResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        public AiReviewTerrorismOcrTaskOutput Output { get; set; }
    }

    #region 内容审核 Ocr 文字鉴恐任务输出。

    public class AiReviewTerrorismOcrTaskOutput
    {
        /// <summary>
        /// Asr 文字涉黄评分，分值为0到100
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// Ocr 文字涉恐结果建议，取值范围：
        /// pass。
        /// review。
        /// block。
        /// </summary>
        public string Suggestion { get; set; }
    }

    #endregion

    #endregion

    #region 视频内容审核 Asr 文字鉴违禁任务的查询结果，当任务类型为 Prohibited.Asr 时有效。

    public class AiReviewTaskProhibitedAsrResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        public AiReviewProhibitedAsrTaskOutput Output { get; set; }
    }

    #region 内容审核 Asr 文字鉴违禁任务输出。

    public class AiReviewProhibitedAsrTaskOutput
    {
        /// <summary>
        ///Asr 文字涉违禁评分，分值为0到100。
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// Asr 文字涉违禁结果建议，取值范围
        /// pass。
        /// review。
        /// block。
        /// </summary>
        public string Suggestion { get; set; }
    }

    #endregion

    #endregion

    #region 视频内容审核 Ocr 文字鉴违禁任务的查询结果，当任务类型为 Prohibited.Ocr 时有效。

    public class AiReviewTaskProhibitedOcrResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        ///40000：输入参数不合法，请检查输入参数；
        ///60000：源文件错误（如视频数据损坏），请确认源文件是否正常；
        ///70000：内部服务错误，建议重试。
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        public AiReviewProhibitedOcrTaskOutput Output { get; set; }
    }

    #region Ocr 文字涉违禁信息

    public class AiReviewProhibitedOcrTaskOutput
    {
        /// <summary>
        ///Ocr 文字涉违禁评分，分值为0到100。
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// Ocr 文字涉违禁结果建议，取值范围：
        /// pass。
        /// review。
        /// block。
        /// </summary>
        public string Suggestion { get; set; }
    }

    #endregion

    #endregion

    #endregion

    #region 视频内容分析任务的执行状态与结果。

    public class AiAnalysisResult
    {
        /// <summary>
        /// 任务的类型，可以取的值有：
        /// Classification：智能分类
        /// Cover：智能封面
        /// Tag：智能标签
        /// FrameTag：智能按帧标签
        /// Highlight：智能精彩集锦
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 视频内容分析智能分类任务的查询结果。
        /// </summary>
        public AiAnalysisTaskClassificationResult ClassificationTask { get; set; }

        /// <summary>
        /// 视频内容分析智能标签任务的查询结果
        /// </summary>
        public AiAnalysisTaskTagResult TagTask { get; set; }
    }

    #region 视频内容分析智能分类任务的查询结果，当任务类型为 Classification 时有效。

    public class AiAnalysisTaskClassificationResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 智能分类任务输出。
        /// </summary>
        public AiAnalysisTaskClassificationOutput Output { get; set; }
    }

    public class AiAnalysisTaskClassificationOutput
    {
        public List<MediaAiAnalysisClassificationItem> ClassificationSet { get; set; }
    }

    /// <summary>
    /// 视频智能分类列表。
    /// </summary>
    public class MediaAiAnalysisClassificationItem
    {
        /// <summary>
        /// 智能分类的类别名称。
        /// </summary>
        public string Classification { get; set; }

        /// <summary>
        /// 智能分类的可信度，取值范围是 0 到 100。
        /// </summary>
        public float Confidence { get; set; }
    }

    #endregion

    #region 视频内容分析智能标签任务的查询结果，当任务类型为 Tag 时有效。

    public class AiAnalysisTaskTagResult
    {
        /// <summary>
        /// 任务状态，有 PROCESSING，SUCCESS 和 FAIL 三种。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误码，0 表示成功，其他值表示失败：
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///智能标签任务输出
        /// </summary>
        public AiAnalysisTaskTagOutput Output { get; set; }
    }

    public class AiAnalysisTaskTagOutput
    {
        /// <summary>
        /// 视频智能标签列表。
        /// </summary>
        public List<MediaAiAnalysisTagItem> TagSet { get; set; }
    }

    public class MediaAiAnalysisTagItem
    {
        /// <summary>
        /// 标签名称。
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 标签的可信度，取值范围是 0 到 100。
        /// </summary>
        public float Confidence { get; set; }
    }

    #endregion

    #endregion

    #endregion
}