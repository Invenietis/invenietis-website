using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.LocalizedRoutes.Mvc
{
    public interface IMvcLocalizedRoute : ILocalizedRoute
    {
        /// <summary>
        /// The controller targeted by the route
        /// </summary>
        string Controller { get; }

        /// <summary>
        /// The controller's action targeted by the route
        /// </summary>
        string Action { get; }
    }
}
