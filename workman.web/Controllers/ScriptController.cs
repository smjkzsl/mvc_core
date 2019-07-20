using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Workman.Plugin;
namespace Workman.web.Controllers
{
 
 
    public class ScriptController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private   string contentRootPath;
        private   string webRootPath;
        public ScriptController(IHostingEnvironment hostingEnvironment)  {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index() 
        {
            webRootPath = _hostingEnvironment.WebRootPath;
            contentRootPath = _hostingEnvironment.ContentRootPath;
            var path = this.RouteData.Values["ControllerName"].ToString() + this.RouteData.Values["ActionName"].ToString();
            this.RouteData.Values["controller"] = this.RouteData.Values["ControllerName"].ToString();
            this.RouteData.Values["action"] = this.RouteData.Values["ActionName"].ToString();
            return PY(this.RouteData);

        }
        private string CurrentModuleName = "";
  
        #region 属性桥接
        public ActionResult ShowView(dynamic data)
        {
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return View();
            }

        }
        
        

        public void SetViewBag(dynamic data)
        {

            ViewBag.data = data;
            // ViewBag[name] = data;
        }
        public dynamic GetViewBag()
        {
            return ViewBag;
        }
        public object GetRequest()
        {
            return this.Request;
        }
        public object GetResponse()
        {
            return this.Response;
        }
        public object GetSession()
        {
            return this.HttpContext.Session;
        }

       
        //public void WriteLog(string info, int type = 3)
        //{
        //    UserInfo userInfo = LoginUserInfo.Get();
        //    LogEntity logEntity = new LogEntity();
        //    logEntity.F_CategoryId = type;
        //    logEntity.F_OperateTypeId = "0";// ((int)OperationType.Visit).ToString();
        //    logEntity.F_OperateType = "其它";
        //    logEntity.F_OperateAccount = userInfo.account;
        //    logEntity.F_OperateUserId = userInfo.userId;
        //    logEntity.F_Module = "(PYTHON)" + "/Scripts/" + CurrentModuleName + "";
        //    logEntity.F_ExecuteResult = type == 4 ? 0 : 1;
        //    logEntity.F_ExecuteResultJson = info;
        //    logEntity.WriteLog();
        //}
        #endregion 属性桥接
        private ActionResult PY(RouteData Route )
        {
            /*
             * 以双下划线开始并以双下划线结束的控制器名 如要访问person.py 则 /__person__/{action}的方式
            */
            string moduleName = Route.Values["controller"].ToString();
            string action_name = Route.Values["action"].ToString();
            string controller_name = "";
            //if (Route["Controller"].ToString() == "Dynamic" && dynamicRoute.Contains("scripts/"))
            //{

            //    string[] tmp = dynamicRoute.Split('/');
            //    if (tmp.Length > 2)
            //    {
            //        moduleName = controller_name = tmp[1];
            //        action_name = tmp[2];
            //    }

            //}
            var s = this.Request.HttpContext;//.RequestContext;
            PythonInstance py = null;

            //moduleName =  moduleName.Replace("__", "");
            //controller_name = controller_name.Replace("__", "");
            string controler_file = contentRootPath + "/Scripts/Controllers/" + moduleName + "/" + moduleName + ".py";

            IList<string> libPath = new List<string>();
            libPath.Add(contentRootPath + "/Scripts/Controllers");
            dynamic ret = null;
            if (System.IO.File.Exists(controler_file))
            {
                controller_name = moduleName;
                try
                {
                    py = new PythonInstance(controler_file, libPath);

                }
                catch (Exception e)
                {
                    //this.WriteLog(e.Message, 4);
                    return Content(e.Message);
                }


                var p =   Route.Values["id"] != null ? Route.Values["id"].ToString() : null;

                try
                {
                    py.run(controller_name);
                    py.CallMethod("initMVC", this);
                }
                catch (Exception e)
                {
                    //this.WriteLog(e.Message, 4);
                    return Content(e.Message);
                }

                try
                {
                    CurrentModuleName = controller_name + "/" + action_name;
                    if (p == null)
                    {
                        ret = py.CallFunction(action_name);
                    }
                    else
                    {
                        ret = py.CallFunction(action_name, p);// py.execute_action(controller_name + "." + action_name);
                    }
                }
                catch (Exception e)
                {
                    //this.WriteLog(e.Message, 4);
                    return Content("模块:" + CurrentModuleName + "(" + e.Message + ")");
                }

                if (ret == null)
                {
                    return Content("");
                }
                if (ret.GetType() ==    typeof(JsonResult) || ret.GetType() == typeof(ActionResult) || ret.GetType() == typeof(ViewResult) || ret.GetType() == typeof(ContentResult))
                {
                    return ret;
                }
            }
            else
            {
                //throw new System.Exception("功能未实现");
                ret = "脚本未找到";
                
                Response.StatusCode = 404;
                //this.WriteLog(ret + ":" + controler_file, 4);

            }
            var x = new ContentResult();
            x.Content = ret;//{new {Content=dynamicRoute}};// View(dynamicRoute);// View(viewFile);
            return x;
        }
 
    }
}