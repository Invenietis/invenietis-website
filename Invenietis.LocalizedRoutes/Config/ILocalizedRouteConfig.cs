using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.LocalizedRoutes.Config
{
    /// <summary>
    /// Configuration model used to generate localized routes
    /// </summary>
    public interface ILocalizedRouteConfig
    {
        IEnumerable<IRouteCulture> Cultures { get; }
    }

    /// <summary>
    /// A supported culture
    /// </summary>
    public interface IRouteCulture
    {
        /// <summary>
        /// Two-letter culture code
        /// </summary>
        string Culture { get; }

        /// <summary>
        /// Id of the default route : basically, the start page of a website
        /// </summary>
        string DefaultRouteId { get; }

        /// <summary>
        /// The different route fragments localized in this culture
        /// </summary>
        IEnumerable<IRouteFragment> RouteFragments { get; }
    }

    /// <summary>
    /// A RouteFragment is an abstract representation of a route part.
    /// </summary>
    public interface IRouteFragment
    {
        /// <summary>
        /// Id of the route fragment. Must be unique by RouteCulture. 
        /// It is convention-based : the dots indicate the inheritance hierarchy.
        /// For example, "Home.Index" fragment's value inherits from "Home" fragment's value.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The localized part of the route
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Indicates if the fragment is abstract. An abstract fragment is never considered as a concrete route, but can be used to compose other routes.
        /// </summary>
        bool IsAbstract { get; }
    }
}
