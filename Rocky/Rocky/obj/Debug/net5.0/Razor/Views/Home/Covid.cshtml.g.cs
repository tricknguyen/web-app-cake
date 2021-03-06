#pragma checksum "D:\.NET\ASP\Rocky\Rocky\Views\Home\Covid.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3a90d0a3524d3f1ad10853dc9208e957555e8c32"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Covid), @"mvc.1.0.view", @"/Views/Home/Covid.cshtml")]
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
#nullable restore
#line 1 "D:\.NET\ASP\Rocky\Rocky\Views\_ViewImports.cshtml"
using Rocky;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\.NET\ASP\Rocky\Rocky\Views\_ViewImports.cshtml"
using Rocky_Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\.NET\ASP\Rocky\Rocky\Views\_ViewImports.cshtml"
using React.Web.Mvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\.NET\ASP\Rocky\Rocky\Views\_ViewImports.cshtml"
using React.AspNet;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3a90d0a3524d3f1ad10853dc9208e957555e8c32", @"/Views/Home/Covid.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17cf6a2d121aa663faf94a9eaebfe00d9b3aebf4", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Covid : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("loading"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3a90d0a3524d3f1ad10853dc9208e957555e8c323759", async() => {
                WriteLiteral(@"
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>
        Covid tracker
    </title>
    <link rel=""shortcut icon"" href=""/image/favicon.png"" type=""image/png"">
    <link rel=""preconnect"" href=""https://fonts.gstatic.com"">
    <link href=""https://fonts.googleapis.com/css2?family=Cairo:wght@200;300;400;600;700;900&display=swap"" rel=""stylesheet"">
    <link href='https://unpkg.com/boxicons@2.0.7/css/boxicons.min.css' rel='stylesheet'>
    <!-- CSS -->
    <link rel=""stylesheet"" href=""/css/covid/grid.css"">
    <link rel=""stylesheet"" href=""/css/covid/app.css"">
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3a90d0a3524d3f1ad10853dc9208e957555e8c325454", async() => {
                WriteLiteral("\r\n\r\n    <!-- top nav -->\r\n    <div class=\"nav-wrapper\" style=\"background-color:#ffdff7!important;\">\r\n        <div class=\"container\">\r\n            <div class=\"nav\">\r\n                <a");
                BeginWriteAttribute("href", " href=\"", 920, "\"", 927, 0);
                EndWriteAttribute();
                WriteLiteral(@" class=""logo"">
                    C<i class=""bx bxs-virus-block bx-tada""></i>VID TRACKER
                </a>
                <div class=""darkmode-switch"" id=""darkmode-switch"">
                    <span>
                        <i class=""bx bxs-moon""></i>
                        <i class=""bx bxs-sun""></i>
                    </span>
                </div>
            </div>
        </div>
    </div>
    <!-- end top nav -->
    <!-- main content -->
    <div class=""content"">
        <div class=""container"">
            <!-- country select -->
            <div class=""row"">
                <div class=""col-3 col-md-6 col-sm-12"">
                    <div class=""box"">
                        <div class=""country-select"" id=""country-select"">
                            <div class=""country-select-toggle"" id=""country-select-toggle"">
                                <span>
                                    Global
                                </span>
                                <i class=");
                WriteLiteral(@"""bx bx-chevron-down""></i>
                            </div>
                            <div class=""country-select-list"" id=""country-select-list"">
                                <input type=""text"" placeholder=""Search country name"">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end country select -->
            <!-- tracking information -->
            <div class=""row"">
                <!-- left content -->
                <div class=""col-8 col-md-12 col-sm-12"">
                    <div class=""row"">
                        <!-- counter -->
                        <div class=""col-4 col-md-4 col-sm-12"">
                            <div class=""box box-hover"">
                                <div class=""count count-confirmed"">
                                    <div class=""count-icon"">
                                        <i class=""bx bxs-virus""></i>
                                ");
                WriteLiteral(@"    </div>
                                    <div class=""count-info"">
                                        <h5 id=""confirmed-total"">123,456,789</h5>
                                        <span>confirmed</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- counter -->
                        <!-- counter -->
                        <div class=""col-4 col-md-4 col-sm-12"">
                            <div class=""box box-hover"">
                                <div class=""count count-recovered"">
                                    <div class=""count-icon"">
                                        <i class=""bx bxs-smile""></i>
                                    </div>
                                    <div class=""count-info"">
                                        <h5 id=""recovered-total"">123,456,789</h5>
                                        <span>recovered<");
                WriteLiteral(@"/span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- counter -->
                        <!-- counter -->
                        <div class=""col-4 col-md-4 col-sm-12"">
                            <div class=""box box-hover"">
                                <div class=""count count-death"">
                                    <div class=""count-icon"">
                                        <i class=""bx bxs-sad""></i>
                                    </div>
                                    <div class=""count-info"">
                                        <h5 id=""death-total"">123,456,789</h5>
                                        <span>deaths</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- counter -->
                        ");
                WriteLiteral(@"<!-- all time chart -->
                        <div class=""col-12"">
                            <div class=""box"">
                                <div class=""box-header"">
                                    all times
                                </div>
                                <div class=""box-body"">
                                    <div id=""all-time-chart""></div>
                                </div>
                            </div>
                        </div>
                        <!-- end all time chart -->
                        <!-- embed video -->
                        <div class=""col-6 col-md-6 col-sm-12"">
                            <div class=""box"">
                                <div class=""box-header"">
                                    what is covid-19
                                </div>
                                <div class=""box-body"">
                                    <iframe width=""100%"" height=""315"" src=""https://www.youtube.com/embed/OZcRD");
                WriteLiteral(@"9fV7jo"" title=""YouTube video player"" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"" allowfullscreen></iframe>
                                </div>
                            </div>
                        </div>
                        <!-- end embed video -->
                        <!-- embed video -->
                        <div class=""col-6 col-md-6 col-sm-12"">
                            <div class=""box"">
                                <div class=""box-header"">
                                    how to protect yourself
                                </div>
                                <div class=""box-body"">
                                    <iframe width=""100%"" height=""315"" src=""https://www.youtube.com/embed/6XdjmB4IY3M"" title=""YouTube video player"" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"" allowfullscreen></iframe>
                              ");
                WriteLiteral(@"  </div>
                            </div>
                        </div>
                        <!-- end embed video -->
                    </div>
                </div>
                <!-- end left content -->
                <!-- right content -->
                <div class=""col-4 col-md-12 col-sm-12"">
                    <!-- 30 days chart -->
                    <div class=""box"">
                        <div class=""box-header"">
                            last 30 days
                        </div>
                        <div class=""box-body"">
                            <div id=""days-chart""></div>
                        </div>
                    </div>
                    <!-- end 30 days chart -->
                    <!-- recovery rate chart -->
                    <div class=""box"">
                        <div class=""box-header"">
                            recovery rate
                        </div>
                        <div class=""box-body"">
                      ");
                WriteLiteral(@"      <div id=""recover-rate-chart""></div>
                        </div>
                    </div>
                    <!-- end recovery rate chart -->
                    <!-- top country affected -->
                    <div class=""box"">
                        <div class=""box-header"">
                            top countries affected
                        </div>
                        <div class=""box-body"">
                            <table class=""table-countries"" id=""table-countries"">
                                <thead>
                                    <tr>
                                        <th>
                                            Country
                                        </th>
                                        <th>Confirmed</th>
                                        <th>Recovered</th>
                                        <th>Deaths</th>
                                    </tr>
                                </thead>
                      ");
                WriteLiteral(@"          <tbody>
                                    <tr>
                                        <td>test</td>
                                        <td>test</td>
                                        <td>test</td>
                                        <td>test</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <!-- end top country affected -->
                </div>
                <!-- end right content -->
            </div>
            <!-- end tracking information -->
        </div>
    </div>
    <!-- end main content -->
    <!-- footer -->
    
    <!-- end footer -->
    <!-- loader -->
    <div class=""loader"">
        <i class=""bx bxs-virus bx-spin""></i>
    </div>
    <!-- end loader -->
    <!-- apexchart -->
    <script src=""https://cdn.jsdelivr.net/npm/apexcharts""></script>
    <!-- JS -->
    <script src=""/j");
                WriteLiteral("s/Covid/fetchApi.js\"></script>\r\n    <script src=\"/js/Covid/covidApi.js\"></script>\r\n    <script src=\"/js/Covid/app.js\"></script>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n</html>");
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
