using AspectCore.DynamicProxy.Parameters;
using System;
using System.Threading.Tasks;
using TM.Core.Aspects.Base;
using TM.Infrastructure.Extensions.Common;

namespace TM.Core.Aspects
{
    /// <summary>
    /// 验证不能为空
    /// </summary>
    public class NotEmptyAttribute : ParameterInterceptorBase
    {
        /// <summary>
        /// 执行
        /// </summary>
        public override Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            if (string.IsNullOrWhiteSpace(context.Parameter.Value.SafeString()))
            {
                throw new ArgumentNullException(context.Parameter.Name);
            }

            return next(context);
        }
    }
}
