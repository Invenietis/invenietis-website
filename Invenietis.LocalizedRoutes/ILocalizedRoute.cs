using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.LocalizedRoutes
{
    /// <summary>
    /// The concretized localized route, built from the configuration
    /// </summary>
    public interface ILocalizedRoute
    {
        /// <summary>
        /// The route culture
        /// </summary>
        string Culture { get; }

        /// <summary>
        /// The initial specified RouteId
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The final localized route name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The final localized route template
        /// </summary>
        string Template { get; }

        /// <summary>
        /// Indicates if this route is the default route for this culture
        /// </summary>
        bool IsDefault { get; }
    }
}
