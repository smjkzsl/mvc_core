#pragma checksum "d:\code\mvc_core\workman.web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6e3cb15412b6c62f7d74bd23743d2b8804bb5f51"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "d:\code\mvc_core\workman.web\Views\_ViewImports.cshtml"
using workman.web;

#line default
#line hidden
#line 2 "d:\code\mvc_core\workman.web\Views\_ViewImports.cshtml"
using workman.web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e3cb15412b6c62f7d74bd23743d2b8804bb5f51", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db98806ee76ced8e04ea7a3a0b20084f0f253eb2", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "d:\code\mvc_core\workman.web\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";
    Layout="_LayoutTop";

#line default
#line hidden
            BeginContext(71, 899, true);
            WriteLiteral(@"<script>
    $(function () {
            var page = {
                init: function () {
                    this.load();
                    this.bind();
                    this.userInit();
                },
                load: function () {
                    

                },
                bind: function () {

                    layui.config({
                        base: top.$.rootUrl + '/content/layui/' //静态资源所在路径
                    }).extend({
                        index: 'lib/index' //主入口模块
                    }).use('index');
                    return;

                },
                userInit: function () {

                },
                loginout: function () { // 安全退出

                },
                clearredis: function () {

                },

            };
            page.init();

        });



</script>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591