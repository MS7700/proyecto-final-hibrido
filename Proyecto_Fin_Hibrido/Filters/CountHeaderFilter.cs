using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Proyecto_Fin_Hibrido.Filters
{
    public class CountHeaderFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request.Properties.ContainsKey("Count"))
            {
                var count = actionExecutedContext.Request.Properties["Count"];
                actionExecutedContext.Response.Content.Headers.ContentRange = new System.Net.Http.Headers.ContentRangeHeaderValue(int.Parse(count.ToString()));
                actionExecutedContext.Response.Content.Headers.ContentRange.Unit = "posts";
                actionExecutedContext.Response.Content.Headers.Add("X-Total-Count", count.ToString());
                //actionExecutedContext.Response.Content.Headers.Add("Content-Range", count.ToString());
            }
        }
    }


}