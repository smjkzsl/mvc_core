using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace Workman.Plugin
{
    public class ScriptRoute : IRouter
    {
        private readonly string  _urls;
        private readonly IRouter _mvcRoute;
        Regex re = null;

        public ScriptRoute(IServiceProvider services, params string[] urls)
        {
            _urls = urls[0];
            re = new Regex(_urls);
            _mvcRoute = services.GetRequiredService<MvcRouteHandler>();
        }

        public async Task RouteAsync(RouteContext context)
        {
            var requestedUrl = context.HttpContext.Request.Path.Value.TrimEnd('/');
            if (!string.IsNullOrEmpty(requestedUrl))
            {
                var mc = re.Match(requestedUrl);
                if (mc.Captures.Count>0 && mc.Groups.Count > 0)
                {
                    context.RouteData.Values["controller"] = "Script";
                    context.RouteData.Values["action"] = "Index";
                    context.RouteData.Values["id"] = mc.Groups["id"];
                    context.RouteData.Values["ControllerName"] = mc.Groups["controller"];
                    var _action = mc.Groups["action"].ToString();
                    if (string.IsNullOrEmpty(_action))
                    {
                        _action = "Index";
                    }
                    context.RouteData.Values["ActionName"] = _action;
                    
                    await _mvcRoute.RouteAsync(context);
                }
                
            }
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            if (context.Values.ContainsKey("legacyUrl"))
            {
                var url = context.Values["legacyUrl"] as string;
                if (_urls.Contains(url))
                {
                    return new VirtualPathData(this, url);
                }
            }
            return null;
        }
    }
}