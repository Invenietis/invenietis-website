using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.LocalizedRoutes.Mvc
{
    public class MvcLocalizedRoute : LocalizedRoute, IMvcLocalizedRoute
    {
        public MvcLocalizedRoute()
        {

        }

        public string Controller { get; set; }

        public string Action { get; set; }
    }
}
