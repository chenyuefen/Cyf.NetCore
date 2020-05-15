using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cyf.NetCore.Interface;
using Cyf.NetCore.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cyf.NetCore.Controllers
{
    public class SecondController : Controller
    {
        #region MyRegion
        private ILoggerFactory _Factory = null;
        private ILogger<SecondController> _logger = null;
        private ITestServiceA _ITestServiceA = null;
        private ITestServiceB _ITestServiceB = null;
        private ITestServiceC _ITestServiceC = null;
        private ITestServiceD _ITestServiceD = null;
        private IA _IA = null;
        public ITestServiceA ITestServiceA { get; set; }
        public SecondController(ILoggerFactory factory,
            ILogger<SecondController> logger,
            ITestServiceA testServiceA,
            ITestServiceB testServiceB,
            ITestServiceC testServiceC,
            ITestServiceD testServiceD,
            IA a)
        {
            this._Factory = factory;
            this._logger = logger;
            this._ITestServiceA = testServiceA;
            this._ITestServiceB = testServiceB;
            this._ITestServiceC = testServiceC;
            this._ITestServiceD = testServiceD;
            this._IA = a;
        }
        #endregion


        public IActionResult Index()
        {
            this._logger.LogError("这里是ILogger<SecondController> Error");
            this._Factory.CreateLogger<SecondController>().LogError("这里是ILoggerFactory Error");

            this._logger.LogWarning($"_ITestServiceA={this._ITestServiceA.GetHashCode()}");
            this._logger.LogWarning($"_ITestServiceB={this._ITestServiceB.GetHashCode()}");
            this._logger.LogWarning($"_ITestServiceC={this._ITestServiceC.GetHashCode()}");
            this._logger.LogWarning($"_ITestServiceD={this._ITestServiceD.GetHashCode()}");

            this._ITestServiceB.Show();

            this._IA.Show(123, "走自己的路");


            return View();
        }
    }
}