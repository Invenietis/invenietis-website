using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.LocalizedRoutes;
using Invenietis.LocalizedRoutes.Mvc;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Invenietis.Web.Controllers
{
    public class LocalizationController : Controller
    {
        LocalizedRouteProvider _routeProvider;

        public LocalizationController( LocalizedRouteProvider routeProvider )
        {
            _routeProvider = routeProvider;
        }

        public IActionResult Index()
        {
            var culture = CultureInfo.CurrentCulture.Name;

            var routeName = _routeProvider.GetLocalizedRoute(x => x.IsDefault, culture).Name;

            return RedirectToRoute( routeName );
        }
    }
}
