using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballManager {
    public static class HtmlHelpers {
         /// <summary>  
         /// Creates an Html helper for an Image  
         /// </summary>   
         /// <param name="helper"></param>   
         /// <param name="src"></param>   
         /// <param name="altText"></param>   
         /// <param name="class"></param>   
         /// <returns></returns>   
   
         public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, string @class = null) {   
             var builder = new TagBuilder("img");   
             builder.MergeAttribute("src", src);   
             builder.MergeAttribute("alt", altText);
             if (@class != null)
                builder.MergeAttribute("class", @class);   
             return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));   
         }    

    }
}