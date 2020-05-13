using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cyf.NetCore.Controllers
{
    /// <summary>
    /// *********** 运行时修改自动重新编译 ***********
    /// -> nuget增加 Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation 引用
    /// -> Startup[ConfigureServices] 增加 .AddRazorRuntimeCompilation();
    /// </summary>
    public class FirstController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}