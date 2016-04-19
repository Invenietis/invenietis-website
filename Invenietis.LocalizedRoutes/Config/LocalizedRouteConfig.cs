using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.LocalizedRoutes.Config
{
    public class LocalizedRouteConfig : ILocalizedRouteConfig
    {
        public RouteCulture[] Cultures { get; set; }

        IEnumerable<IRouteCulture> ILocalizedRouteConfig.Cultures
        {
            get { return Cultures; }
        }
    }

    public class RouteCulture : IRouteCulture
    {
        public string Culture { get; set; }

        public string DefaultRouteId { get; set; }

        public RouteFragment[] RouteFragments { get; set; }

        IEnumerable<IRouteFragment> IRouteCulture.RouteFragments
        {
            get { return RouteFragments; }
        }
    }

    public class RouteFragment : IRouteFragment
    {
        public string Id { get; set; }

        public string Value { get; set; }

        public bool IsAbstract { get; set; }
    }
}
