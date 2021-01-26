using System.ComponentModel;

namespace TM.Infrastructure.TencentCloud.Enums
{
    /// <summary>
    /// 返回码（JSON）
    /// 应该更名为ReturnCode_MP，但为减少项目中的修改，此处依旧用ReturnCode命名
    /// </summary>
    public enum ReturnCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InternalError1 = 1,

        /// <summary>
        /// 无效参数
        /// </summary>
        [Description("无效参数")]
        InternalError1000 = 1000,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InternalError1108 = 1108,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InternalError1152 = 1152,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InternalError1801 = 1801,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InternalError1802 = 1802,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InternalError2000 = 2000,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InternalError2001 = 2001,

        /// <summary>
        /// 文件状态异常
        /// </summary>
        [Description("文件状态异常")]
        FileStatusException = 10009,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InternalError10010 = 10010,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InternalError10012 = 10012,

        /// <summary>
        /// 缺少必要参数，或者参数值格式不正确，具体错误信息请查看错误描述 message 字段。
        /// </summary>
        [Description("请求参数非法")]
        InvalidParameter = 4000,

        /// <summary>
        /// 身份认证失败，一般是由于签名计算错误导致的，请参考文档中签名方法部分。
        /// </summary>
        [Description("身份认证失败")]
        SignatureFailure = 4100,

        /// <summary>
        /// 子账号未被主账号授权访问该接口，请联系主帐号管理员开通接口权限。
        /// </summary>
        [Description("未授权访问接口")]
        AuthFailureInterface = 4101,

        /// <summary>
        /// 子账号未被主账号授权访问特定资源，请联系主帐号管理员开通资源权限。
        /// </summary>
        [Description("未授权访问资源")]
        AuthFailureResource = 4102,

        /// <summary>
        /// 子账号没有被主账户授权访问该接口中所操作的特定资源，请联系主帐号管理员开通资源权限。
        /// </summary>
        [Description("未授权访问当前接口所操作的资源")]
        AuthFailureInResource = 4103,

        /// <summary>
        /// 用于请求的密钥不存在，请确认后重试。
        /// </summary>
        [Description("密钥不存在")]
        SecretIdNotFound = 4104,

        /// <summary>
        /// oken错误。
        /// </summary>
        [Description("token错误")]
        TokenCheckFailed = 4105,

        /// <summary>
        /// MFA校验失败。
        /// </summary>
        [Description("MFA校验失败")]
        MFACheckFailed = 4106,

        /// <summary>
        /// 其他CAM鉴权失败。
        /// </summary>
        [Description("其他CAM鉴权失败")]
        CAMInnerError = 4110,

        /// <summary>
        /// 帐号被封禁，或者不在接口针对的用户范围内等。
        /// </summary>
        [Description("拒绝访问")]
        Forbidden = 4300,

        /// <summary>
        /// 请求的次数超过了配额限制，请参考文档请求配额部分。
        /// </summary>
        [Description("超过配额")]
        LimitExceeded = 4400,

        /// <summary>
        /// 请求的 Nonce 和 Timestamp 参数用于确保每次请求只会在服务器端被执行一次,所以本次的 Nonce 和上次的不能重复, Timestamp 与腾讯服务器相差不能超过5分钟。
        /// </summary>
        [Description("重放攻击")]
        ReplayAttack = 4500,

        /// <summary>
        /// 协议不支持，当前API仅支持https协议，不支持http协议。
        /// </summary>
        [Description("协议不支持")]
        UnsupportedProtocol = 4600,

        /// <summary>
        /// 资源标识对应的实例不存在，或者实例已经被退还，或者访问了其他用户的资源。
        /// </summary>
        [Description("资源不存在")]
        NotFoundResources = 5000,

        /// <summary>
        /// 对资源的操作失败，具体错误信息请查看错误描述 message 字段，稍后重试或者联系客服人员帮忙解决。
        /// </summary>
        [Description("资源操作失败")]
        ResourceOpFailed = 5100,

        /// <summary>
        /// 购买资源失败，可能是不支持实例配置，资源不足等等。
        /// </summary>
        [Description("资源购买失败")]
        ResourcePurchaseFailure = 5200,

        /// <summary>
        /// 用户帐号余额不足，无法完成购买或升级。
        /// </summary>
        [Description("余额不足")]
        InsufficientBalance = 5300,

        /// <summary>
        /// 批量操作部分执行成功, 详情见方法返回值。
        /// </summary>
        [Description("部分执行成功")]
        PartialSuccess = 5400,

        /// <summary>
        /// 购买资源失败，用户资质审核未通过。
        /// </summary>
        [Description("用户资质审核未通过")]
        UserQualificationAuditFailed = 5500,

        /// <summary>
        /// 服务器内部出现错误，请稍后重试或者联系客服人员帮忙解决。
        /// </summary>
        [Description("服务器内部错误")]
        InternalError = 6000,

        /// <summary>
        /// 本版本内不支持此接口或该接口处于维护状态等。注意: 出现这个错误时, 请先确定接口的域名是否正确, 不同的模块, 域名可能不一样。
        /// </summary>
        [Description("版本暂不支持")]
        NotFound = 6100,

        /// <summary>
        /// 当前接口处于停服维护状态，请稍后重试。
        /// </summary>
        [Description("接口暂时无法访问")]
        ActionUnavailable = 6200,
    }
}
