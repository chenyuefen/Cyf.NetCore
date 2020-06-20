using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Serilog;
using System.Linq;
//using Serilog;
//using Serilog.Core;

namespace Helpers
{
    ///// <summary>
    ///// 记录花费的时间
    ///// </summary>
    //public class LogSpendTimeAdvice : Attribute, IMethodAdvice
    //{
    //    private static Lazy<ILogger> _loggerLazy = new Lazy<ILogger>(() => Log.ForContext<LogSpendTimeAdvice>());
    //    private static ILogger _logger => _loggerLazy.Value;

    //    public void Advise(MethodAdviceContext context)
    //    {
    //        var sw = Stopwatch.StartNew();
    //        context.Proceed();
    //        try
    //        {
    //            _logger.Information($"{context.TargetType.Name}.{context.TargetMethod.Name}({string.Join(",", context.Arguments.Select(x => x?.ToString()))})|cost {sw.Elapsed.TotalMilliseconds:N0}ms");
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.Error(ex, "统计时间异常");
    //        }
    //    }
    //}
}
