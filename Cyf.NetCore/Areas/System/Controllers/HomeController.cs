using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cyf.NetCore.Areas.System.Controllers
{
    /// <summary>
    /// *********** 使用区域Area ***********
    /// -> 必需在控制器上增加[Area][Route]两个特性
    /// -> 在[Startup][Configure]下增加MapAreaControllerRoute映射
    /// </summary>
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}