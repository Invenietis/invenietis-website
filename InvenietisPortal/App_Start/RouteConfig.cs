using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using RouteMagic;
using MvcApplication1.App_Start;
using System.Globalization;

namespace MvcApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute( "favicon.ico" );
            routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

            //routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);


            //routes.MapRoute(
            //    name: "Default",
            //    url: "{action}",
            //    defaults: new { lang = "fr", controller = "Home", action = "Index" }
            //);

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);


            const string defautlRouteUrl = "{action}";
            RouteValueDictionary defaultRouteValueDictionary = new RouteValueDictionary( new { controller = "Home", action = "Index" } );
            RouteValueDictionary blogRoute = new RouteValueDictionary( new { controller = "Blog", action = "BlogIndex" } );
            //Route defaultRoute = new Route( defautlRouteUrl, defaultRouteValueDictionary, new MvcRouteHandler() );


            //routes.Add( "HomeLocalized", new LocalizedRoute( "", defaultRouteValueDictionary ) );
            routes.Add( "DefaultLocalized", new LocalizedRoute( defautlRouteUrl, defaultRouteValueDictionary ) );
            routes.Add( "BlogLocalized", new LocalizedRoute( defautlRouteUrl, blogRoute ) );
            routes.MapDelegate( "Default", defautlRouteUrl, ( rq ) =>
                {
                    var userLanguages = rq.HttpContext.Request.UserLanguages;
                    if( userLanguages != null && userLanguages.Length > 0 )
                    {
                        try
                        {
                            var ci = new CultureInfo( userLanguages[0] );
                            if( ci != null )
                            {
                                rq.RouteData.Values[LocalizedRoute.CultureKey] = ci.TwoLetterISOLanguageName;
                            }
                            else
                            {
                                rq.RouteData.Values[LocalizedRoute.CultureKey] = CultureManager.DefaultCulture.TwoLetterISOLanguageName;
                            }
                        }
                        catch( CultureNotFoundException )
                        {
                            rq.RouteData.Values[LocalizedRoute.CultureKey] = CultureManager.DefaultCulture.TwoLetterISOLanguageName;
                        }
                    }
                    else
                    {
                        rq.RouteData.Values[LocalizedRoute.CultureKey] = CultureManager.DefaultCulture.TwoLetterISOLanguageName;
                    }
                    rq.HttpContext.Response.Redirect( RouteTable.Routes.GetVirtualPath( rq, null ).VirtualPath, false );
                } ).Defaults = defaultRouteValueDictionary;


            //routes.Add( "Default", new Route( defautlRouteUrl, defaultRouteValueDictionary, new MvcRouteHandler() ) );

        }
    }
}