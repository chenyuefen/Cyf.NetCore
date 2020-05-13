using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cyf.NetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cyf.NetCore.Controllers
{
    /// <summary>
    /// *********** 运行时修改自动重新编译 ***********
    /// -> nuget增加 Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation 引用
    /// -> Startup[ConfigureServices] 增加 .AddRazorRuntimeCompilation();
    /// 
    /// *********** 四种传值方式：ViewData,ViewBag,TempData,Model, TempData有坑***********
    /// tempdata序列化时，只能是基础类型+int集合+字典，不能是自定义类型
    /// </summary>
    public class FirstController : Controller
    {
        public IActionResult Index(int? id)
        {
            base.ViewData["User1"] = new CurrentUser()
            {
                Id = 7,
                Name = "Y",
                Account = " ╰つ Ｈ ♥. 花心胡萝卜",
                Email = "莲花未开时",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            };
            base.ViewData["Something"] = 12345;

            base.ViewBag.Name = "Eleven";
            base.ViewBag.Description = "Teacher";
            base.ViewBag.User = new CurrentUser()
            {
                Id = 7,
                Name = "IOC",
                Account = "限量版",
                Email = "莲花未开时",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            };

            base.TempData["User"] = Newtonsoft.Json.JsonConvert.SerializeObject(new CurrentUser()
            {
                Id = 7,
                Name = "CSS",
                Account = "季雨林",
                Email = "KOKE",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            });//要么就是做成字典 要么就序列化


            if (id == null)
            {
                return this.Redirect("~/First/TempDataPage");
            }
            else
                return View(new CurrentUser()
                {
                    Id = 7,
                    Name = "一点半",
                    Account = "季雨林",
                    Email = "KOKE",
                    Password = "落单的候鸟",
                    LoginTime = DateTime.Now
                });
        }

        public ActionResult TempDataPage()
        {
            base.ViewBag.User = base.TempData["User"];//可以拿到数据
            return View();
        }
    }
}