using System.Web;
using System.Web.Mvc;
using InvPortal.App_Start;

namespace InvPortal
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