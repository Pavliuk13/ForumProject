#pragma checksum "D:\Programming\ForumProject\Views\Shared\_IndividualPostCart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ef0b7118a886753474190ee14fedd0f0a3617fa0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__IndividualPostCart), @"mvc.1.0.view", @"/Views/Shared/_IndividualPostCart.cshtml")]
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
#line 1 "D:\Programming\ForumProject\Views\_ViewImports.cshtml"
using ForumProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Programming\ForumProject\Views\_ViewImports.cshtml"
using ForumProject.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef0b7118a886753474190ee14fedd0f0a3617fa0", @"/Views/Shared/_IndividualPostCart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f3638d970734c70a7dfc918e44333a87782fc2a6", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__IndividualPostCart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ForumProject.Models.AppDBContext.Post>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark form-control btn-lg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height: 50px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div");
            BeginWriteAttribute("class", " class=\"", 54, "\"", 132, 5);
            WriteAttributeValue("", 62, "col-lg-12", 62, 9, true);
            WriteAttributeValue(" ", 71, "col-md-12", 72, 10, true);
            WriteAttributeValue(" ", 81, "pb-4", 82, 5, true);
            WriteAttributeValue(" ", 86, "filter", 87, 7, true);
#nullable restore
#line 4 "D:\Programming\ForumProject\Views\Shared\_IndividualPostCart.cshtml"
WriteAttributeValue(" ", 93, Model.Category.Name.Replace(' ', '_'), 94, 38, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n    <div class=\"card mt-2\">\r\n        <div class=\"card-header row\">\r\n            <div class=\"col-12 col-md-6\">\r\n                <label style=\"font-weight: bold\">");
#nullable restore
#line 8 "D:\Programming\ForumProject\Views\Shared\_IndividualPostCart.cshtml"
                                            Write(Model.Theme);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n            </div>\r\n            <div class=\"col-12 col-md-6\">\r\n                <label><span class=\"text-info h5\">");
#nullable restore
#line 11 "D:\Programming\ForumProject\Views\Shared\_IndividualPostCart.cshtml"
                                             Write(Model.Category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></label>\r\n            </div>\r\n        </div>\r\n        <div class=\"card-body\">\r\n            <div class=\"container rounded p-2\">\r\n                <div class=\"row\">\r\n                    <img class=\"col-12 col-lg-4 p-1 text-center\"");
            BeginWriteAttribute("src", " src=\"", 685, "\"", 722, 2);
#nullable restore
#line 17 "D:\Programming\ForumProject\Views\Shared\_IndividualPostCart.cshtml"
WriteAttributeValue("", 691, WebConst.ImagePath, 691, 19, false);

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Programming\ForumProject\Views\Shared\_IndividualPostCart.cshtml"
WriteAttributeValue("", 710, Model.Image, 710, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Card image cap\"/>\r\n                    <div class=\"col-12 col-lg-8\">\r\n                        <div class=\"row pl-3\">\r\n                            <div class=\"col-12\">\r\n                                <p>");
#nullable restore
#line 21 "D:\Programming\ForumProject\Views\Shared\_IndividualPostCart.cshtml"
                              Write(Html.Raw(Model.Description.Substring(0, (int) (Model.Description.Length > 700 ? 700 : Model.Description.Length))));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"...</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class=""card-footer"">
            <div class=""row"">
                <div class=""col-12 col-md-12 pb-1 "">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef0b7118a886753474190ee14fedd0f0a3617fa07312", async() => {
                WriteLiteral("Read Post");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 31 "D:\Programming\ForumProject\Views\Shared\_IndividualPostCart.cshtml"
                                              WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ForumProject.Models.AppDBContext.Post> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
