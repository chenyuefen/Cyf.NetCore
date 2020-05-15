using Cyf.NetCore.Interface;
using System;

namespace Cyf.NetCore.Servcie
{
    public class TestServiceA : ITestServiceA
    {
        public void Show()
        {
            Console.WriteLine("A123456");
        }
    }
}
