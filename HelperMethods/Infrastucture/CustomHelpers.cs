using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Infrastucture
{
    public static class CustomHelpers
    {
        public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] list)
        {
            var tag = new TagBuilder("ul");
            foreach (var str in list)
            {
                var itemTag = new TagBuilder("li");
                itemTag.SetInnerText(str);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString DisplayMessage(this HtmlHelper html, string msg)
        {
            var result = string.Format("This is the message: <p>{0}</p>", msg);
            return new MvcHtmlString(result);
        }

        public static MvcHtmlString EncodeMessage(this HtmlHelper html, string msg)
        {
            var encodedMessage = html.Encode(msg);
            var result = string.Format("This is the message: <p>{0}</p>", encodedMessage);
            return new MvcHtmlString(result);
        }
    }
}