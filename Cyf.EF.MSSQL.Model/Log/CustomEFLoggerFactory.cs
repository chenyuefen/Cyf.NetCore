using System;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Cyf.EF.MSSQL.Model.Log
{
    public class CustomEFLoggerFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {

        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomEFLogger(categoryName);
        }

        public void Dispose()
        {

        }
    }

}
