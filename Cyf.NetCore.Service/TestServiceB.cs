using Microsoft.Extensions.Logging;
using Cyf.NetCore.Interface;
using System;

namespace Cyf.NetCore.Servcie
{
    public class TestServiceB : ITestServiceB
    {
        private ILogger<TestServiceB> _logger = null;//logger是单独的扩展的
        public TestServiceB(ITestServiceA iTestService, ILogger<TestServiceB> logger)
        {
            this._logger = logger;
        }

        public void Show()
        {
            this._logger.LogDebug($"This is TestServiceB B123456");
            Console.WriteLine($"This is TestServiceB B123456");
        }
    }
}
