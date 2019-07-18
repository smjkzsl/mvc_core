using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using workman.web.Models;
using workman.web.plugin;
namespace workman.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult DesktopIndex(){
            PythonScript pyEngin = new PythonScript();
            string code = @"
class Test:
    def add(self,a,b):
        return a+b
            ";
            pyEngin.init(code);
            pyEngin.run("Test");
            var ret = pyEngin.CallFunction("add", 4, 5);
            ViewData["ret"] = ret;

            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
