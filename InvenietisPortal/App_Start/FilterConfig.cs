using System.Web;
using System.Web.Mvc;
using MvcApplication1.App_Start;

namespace MvcApplication1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters( GlobalFilterCollection filters )
        {
            filters.Add( new HandleErrorAttribute() );
            filters.Add( new RemoveDuplicateContentAttribute() );
        }
    }
}