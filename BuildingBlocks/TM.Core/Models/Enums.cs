﻿using System.ComponentModel;

namespace TM.Core.Models
{
    /// <summary>
    /// 基础状态码
    /// </summary>
    public enum Code
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        [Description("请求成功")]
        Ok = 200,
        /// <summary>
        /// 未授权登录而被拒绝
        /// </summary>
        [Description("该操作需要授权登录后才能继续进行")]
        Unauthorized = 401,
        /// <summary>
        /// 验证Token不存在或已超时
        /// </summary>
        [Description("验证Token已超时，请刷新令牌")]
        TokenInvalid = 402,
        /// <summary>
        /// 已登录，但权限不足
        /// </summary>
        [Description("当前用户权限不足，不能继续执行")]
        Forbidden = 403,
        /// <summary>
        /// 找不到指定资源
        /// </summary>
        [Description("指定的功能不存在")]
        NoFound = 404,
        /// <summary>
        /// HTTP请求类型不合法
        /// </summary>
        [Description("HTTP请求类型不合法")]
        MethodNotAllowed = 405,
        /// <summary>
        /// HTTP请求不合法,请求参数可能被篡改
        /// </summary>
        [Description("HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,
        /// <summary>
        /// 资源被锁定
        /// </summary>
        [Description("指定的功能被锁定")]
        Locked = 423,
        /// <summary>
        /// 请求失败/权限检查出现错误
        /// </summary>
        [Description("请求失败/权限检查出现错误")]
        Error = -1,
        /// <summary>
        /// 服务异常
        /// </summary>
        [Description("服务异常")]
        InternalServerError = 500,
        /// <summary>
        /// 机器未注册
        /// </summary>
        [Description("机器未注册")]
        MachineNoRegister = 1001,

        /// <summary>
        /// 请求成功,无数据
        /// </summary>
        [Description("无数据")]
        NoContent = 204
    }


    /// <summary>
    /// 排序方式
    /// </summary>
    public enum DbOrderBy
    {
        /// <summary>
        /// 顺序Asc
        /// </summary>
        [Description("排序Asc")]
        Asc = 1,
        /// <summary>
        /// 倒序Desc
        /// </summary>
        [Description("排序Desc")]
        Desc = 2
    }

    /// <summary>
    /// 应用枚举
    /// </summary>
    public enum AppType
    {
        /// <summary>
        /// 人脸皮肤检测仪
        /// </summary>
        SkinSer = 1,
        /// <summary>
        /// 王牌BA
        /// </summary>
        GloryBA = 2,
        /// <summary>
        /// 天美支付平台
        /// </summary>
        Payment = 3

    }
}
