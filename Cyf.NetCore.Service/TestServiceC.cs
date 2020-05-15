using Cyf.NetCore.Interface;
using System;

namespace Cyf.NetCore.Servcie
{
    public class TestServiceC : ITestServiceC
    {
        public TestServiceC(ITestServiceB iTestServiceB)
        {
        }
        public void Show()
        {
            Console.WriteLine("C123456");
        }
    }
}
