using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//this is a disaster and i need to learn how to do this better
namespace Assignment10.Infrastructure
{
    //create a pagination class
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory _UrlHelper; //this helps to build routes
        
        public PaginationTagHelper(IUrlHelperFactory uhf) //create a constructor
        {
            _UrlHelper = uhf;
        }

        public Pagination PageInfo { get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext vc { get; set; }

        //classes for the link tags
        public string pageClassSelected { get; set; }     
        public string pageClassNormal { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper UrlHelp = _UrlHelper.GetUrlHelper(vc);

            TagBuilder outputTag = new TagBuilder("div");

            for (int i = 1; i <= PageInfo.Pages; i++)
            {
                TagBuilder link = new TagBuilder("a");

                KeyValuePairs["pagenum"] = i;

                link.Attributes["href"] = UrlHelp.Action("Index", KeyValuePairs);
                link.InnerHtml.AppendHtml(i.ToString());
                link.AddCssClass(i == PageInfo.CurrPage ? pageClassSelected : pageClassNormal);

                outputTag.InnerHtml.AppendHtml(link);
            }

            output.Content.AppendHtml(outputTag.InnerHtml);
        }
    }
}
