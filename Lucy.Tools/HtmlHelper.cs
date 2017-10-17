using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lucy.Tools
{
    public static class ScriptHelper
    {
        public static MvcHtmlString ScriptInclude(this HtmlHelper htmlHelper)
        {
            var area = htmlHelper.ViewContext.RouteData.DataTokens["area"];
            var controller = htmlHelper.ViewContext.RouteData.Values["controller"];
            var action = htmlHelper.ViewContext.RouteData.Values["action"];

            var script = string.Empty;
            if (area.GetType() != typeof(System.DBNull))
            {
                string url = "<script src='/Areas/{0}/Views/{1}/Js/{2}.js?r={3}'></script>";
                script = string.Format(url, area, controller, action, new Random().Next());
            }
            else
            {
                string url = "<script src='/Views/{0}/Js/{1}.js?r={2}'></script>";
                script = string.Format(url, controller, action, new Random().Next());
            }

            return new MvcHtmlString(script);
        }

        public static MvcHtmlString ScriptExtend(this HtmlHelper htmlHelper, object param)
        {
            return ScriptExtend(htmlHelper, "Url", param);
        }

        public static MvcHtmlString ScriptExtend(this HtmlHelper htmlHelper, string name, object param)
        {
            string str = "jQuery.extend({" + string.Format("{0}:{1}", name, JsonConvert.SerializeObject(param)) + "});";
            TagBuilder builder = new TagBuilder("script");
            builder.MergeAttribute("type", "text/javascript");
            builder.InnerHtml = str;
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString ScriptList(this HtmlHelper htmlHelper, params string[] param)
        {
            List<string> list = new List<string>();
            foreach (string item in param)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (item.Contains(".css"))
                    {
                        TagBuilder builder = new TagBuilder("link");
                        builder.MergeAttribute("href", htmlHelper.ResolveUrl(item));
                        builder.MergeAttribute("type", "text/css");
                        builder.MergeAttribute("rel", "stylesheet");
                        list.Add(builder.ToString());
                    }
                    else if (item.Contains(".js"))
                    {
                        TagBuilder builder2 = new TagBuilder("script");
                        builder2.MergeAttribute("type", "text/javascript");
                        builder2.MergeAttribute("src", htmlHelper.ResolveUrl(item));
                        list.Add(builder2.ToString());
                    }
                }
            }
            if (list.Count == 0)
            {
                return MvcHtmlString.Empty;
            }
            return MvcHtmlString.Create(string.Join("\n\t", list.ToArray()));
        }

        public static MvcHtmlString CheckBoxEx(this HtmlHelper htmlHelper, string name, string text, bool Checked)
        {
            TagBuilder htmlStr = new TagBuilder("input");
            htmlStr.MergeAttribute("type", "checkbox");
            htmlStr.MergeAttribute("name", name);
            htmlStr.MergeAttribute("id", name);
            htmlStr.MergeAttribute("value", "True");
            if (Checked)
                htmlStr.MergeAttribute("checked", "checked");
            return MvcHtmlString.Create("<label>" + htmlStr.ToString() + text + "</label>");
        }

        public static MvcHtmlString CheckBoxForEx(this HtmlHelper htmlHelper, string name, string text, string value, bool Checked)
        {
            TagBuilder htmlStr = new TagBuilder("input");
            htmlStr.MergeAttribute("type", "checkbox");
            htmlStr.MergeAttribute("name", name);
            htmlStr.MergeAttribute("id", name);
            htmlStr.MergeAttribute("value", value);
            if (Checked)
                htmlStr.MergeAttribute("checked", "checked");
            return MvcHtmlString.Create(htmlStr.ToString() + "&nbsp;&nbsp;" + text);
        }

        public static string ResolveUrl(this HtmlHelper html, string relativeUrl)
        {
            if (relativeUrl == null)
            {
                return null;
            }

            if (!relativeUrl.StartsWith("~"))
            {
                return relativeUrl;
            }
            return new UrlHelper(html.ViewContext.RequestContext).Content(relativeUrl);
        }
    }
}
