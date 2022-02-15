#pragma checksum "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fff8813434ea37045071db2047a000d8cff50a28"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(VismaCase.Pages.Pages_View), @"mvc.1.0.razor-page", @"/Pages/View.cshtml")]
namespace VismaCase.Pages
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
#line 1 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/_ViewImports.cshtml"
using VismaCase;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fff8813434ea37045071db2047a000d8cff50a28", @"/Pages/View.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bf52bcb54579b9ec9cbafa8aa9780c5f67db82c0", @"/Pages/_ViewImports.cshtml")]
    public class Pages_View : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
  
    ViewData["Title"] = "View";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"text-center\">\n    <h1>Oversikt</h1>\n    <h3>Ansatte</h3>\n    <ul style=\"list-style: none;\">\n");
#nullable restore
#line 11 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
         foreach (var employee in Model.Employees)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li>");
#nullable restore
#line 13 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
           Write(employee.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 13 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
                          Write(employee.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
                                              Write(employee.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n");
#nullable restore
#line 14 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\n    <h3>Stillinger</h3>\n    <ul style=\"list-style: none;\">\n");
#nullable restore
#line 18 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
         foreach (var position in Model.Positions)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li>");
#nullable restore
#line 20 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
           Write(position.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 20 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
                          Write(position.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(": ");
#nullable restore
#line 20 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
                                          Write(position.Employee.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 20 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
                                                                       Write(position.Employee.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n");
#nullable restore
#line 21 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\n    <h3>Oppgaver</h3>\n    <ul style=\"list-style: none;\">\n");
#nullable restore
#line 25 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
         foreach (var task in Model.WorkTasks)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li>");
#nullable restore
#line 27 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
           Write(task.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 27 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
                      Write(task.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(": ");
#nullable restore
#line 27 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
                                  Write(task.Employee.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 27 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
                                                           Write(task.Employee.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n");
#nullable restore
#line 28 "/Users/edvingrytnes/Visma Case/Case_Visma_Graduate/VismaCase/Pages/View.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ViewModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ViewModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ViewModel>)PageContext?.ViewData;
        public ViewModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591