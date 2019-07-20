using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workman.Plugin;
namespace Workman.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                //// This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });
            services.AddDistributedMemoryCache();//启用session之前必须先添加内存
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);//设置session的过期时间
                options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
            }
            );

            services.AddMvc()
                //.AddViewOptions(options => { options.ViewEngines.Clear();  }) 
                
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1) .AddRazorOptions(opt =>
            {
                //opt.ViewLocationFormats.Clear();// 清空默认的列表
                opt.ViewLocationFormats.Add("/Scripts/Controllers/{1}/Views/{0}.cshtml" ); 
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            //Regex reg = new Regex(@"(?is)<a[^>]*?href=(['""]?)(?<url>[^'""\s>]+)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>");
            //MatchCollection mc = reg.Matches(yourStr);
            //foreach (Match m in mc)
            //{
            //    richTextBox2.Text += m.Groups["url"].Value + "\n";//得到href值                
            //    richTextBox2.Text += m.Groups["text"].Value + "\n";//得到<a><a/>中间的内容          
            //}
            app.UseMvc(routes =>
            {
                routes.Routes.Add(new ScriptRoute(
                    app.ApplicationServices,
                    @"^/scripts/(?<controller>[\w]*)/*(?<action>[\w]*)/*(?<id>[\w]*)"
                    )
               );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}")
               //.MapRoute(
               //     name: "script",
               //     template: "scripts/{controller=Script}/{action=Index}/{id?}")
               ;
            });
        }
    }
}
