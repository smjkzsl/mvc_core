using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;

namespace Workman.Util
{
    public static class HttpContext
    {
        private static IHttpContextAccessor _accessor;

        public static Microsoft.AspNetCore.Http.HttpContext Current => _accessor.HttpContext;

        internal static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;


        }
 

        /// <summary>
        /// Sets the object as json.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// Gets the object from json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session">The session.</param>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static string MapPath(this Microsoft.AspNetCore.Http.HttpContext context, string path)
        {
            dynamic type = (new FileDownHelper()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
            if (path == "~/")
            {
                return currentDirectory;
            }
            else
            {
                if (!path.StartsWith("~/"))
                {
                    return Path.Combine(currentDirectory, path);
                }
                else
                {
                    return Path.Combine(currentDirectory, path.Substring(2, path.Length - 1));
                }
            }
        }
    }


    public static class StaticHttpContextExtensions
    {
        public static void MyAddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpContext.Configure(httpContextAccessor);
            return app;
        }
    }


    
}
