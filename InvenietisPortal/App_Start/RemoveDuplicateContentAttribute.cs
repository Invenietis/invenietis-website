using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplication1.App_Start
{
    public class RemoveDuplicateContentAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            var vpd = RouteTable.Routes.GetVirtualPath( filterContext.RequestContext, filterContext.RequestContext.RouteData.Values );
            if( vpd != null )
            {
                var virtualPath = vpd.VirtualPath.ToLower();
                var request = filterContext.RequestContext.HttpContext.Request;

                if( !string.Equals( virtualPath, request.Path ) )
                {
                    filterContext.Result = new RedirectResult( virtualPath + request.Url.Query, true );
                }
            }
            base.OnActionExecuting( filterContext );
        }
    }
}