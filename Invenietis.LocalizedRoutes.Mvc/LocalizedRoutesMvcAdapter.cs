using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;

namespace Invenietis.LocalizedRoutes.Mvc
{
    /// <summary>
    /// Mvc adapter for <see cref="LocalizedRouteProvider"/>.
    /// Controllers needing localized routes must be marked with the <see cref="LocalizedRoutesAttribute"/> attribute.
    /// </summary>
    public class LocalizedRoutesMvcAdapter
    {
        LocalizedRouteProvider _provider;
        List<MvcLocalizedRoute> _routes;
        IRouteBuilder _routeBuilder;
        Assembly _assembly;
        Dictionary<string, IEnumerable<string>> _controllersActionsMap;

        /// <summary>
        /// Initialize a new instance of <see cref="LocalizedRoutesMvcAdapter"/>.
        /// </summary>
        /// <param name="provider">The provider to use</param>
        /// <param name="assembly">The assembly containing the MVC controllers</param>
        public LocalizedRoutesMvcAdapter( LocalizedRouteProvider provider, Assembly assembly )
        {
            if( provider == null ) throw new ArgumentNullException( nameof( provider ) );
            if( assembly == null ) throw new ArgumentNullException( nameof( assembly ) );

            _provider = provider;
            _assembly = assembly;

            _provider.RouteAdded += OnRouteAdded;
            _provider.RouteRemoved += OnRouteRemoved;
            _routes = new List<MvcLocalizedRoute>();
            _controllersActionsMap = GetControllersActionsMap();
        }

        /// <summary>
        /// The list of mapped <see cref="IMvcLocalizedRoute"/>
        /// </summary>
        public IEnumerable<IMvcLocalizedRoute> Routes { get { return _routes; } }

        /// <summary>
        /// Map the localized routes to the corresponding actions of controllers
        /// </summary>
        /// <param name="routeBuilder">The route builder used by the application</param>
        public void MapLocalizedRoutes( IRouteBuilder routeBuilder )
        {
            if( routeBuilder == null ) throw new ArgumentNullException( nameof( routeBuilder ) );
            if( _routes.Count > 0 ) throw new InvalidOperationException( $"Build was already done ({_routes.Count} existing routes)." );

            _routeBuilder = routeBuilder;

            foreach( var ca in _controllersActionsMap )
            {
                var controllerName = GetShortControllerName(ca.Key);

                foreach( var actionName in ca.Value )
                {
                    var mvcId = GetMvcRouteId(controllerName, actionName);

                    var matchingRoutes = _provider.Routes
                        .Where(x => x.Id == mvcId)
                        .Select(x => new MvcLocalizedRoute()
                        {
                            Culture = x.Culture,
                            Id = x.Id,
                            Name = x.Name,
                            IsDefault = x.IsDefault,
                            Template = x.Template,
                            Controller = controllerName,
                            Action = actionName
                        })
                        .ToArray();

                    if( matchingRoutes.Length == 0 ) throw new InvalidOperationException( $"No route found for controller '{controllerName}' and action '{actionName}'" );

                    _routes.AddRange( matchingRoutes );

                    MapLocalizedRoutes( matchingRoutes );
                }
            }
        }

        private void OnRouteAdded( object sender, LocalizedRouteEventArgs args )
        {
            // TODO
            throw new NotImplementedException();
        }

        private void OnRouteRemoved( object sender, LocalizedRouteEventArgs args )
        {
            // TODO
            throw new NotImplementedException();
        }

        private string GetMvcRouteId( string controllerName, string actionName )
        {
            return $"{controllerName}.{actionName}";
        }

        private void MapLocalizedRoutes( IEnumerable<MvcLocalizedRoute> routes )
        {
            foreach( var r in routes )
            {
                _routeBuilder.MapRoute(
                    name: r.Name,
                    template: r.Template,
                    defaults: new { controller = r.Controller, action = r.Action }
                );
            }
        }

        private Dictionary<string, IEnumerable<string>> GetControllersActionsMap()
        {
            var assemblyTypes = _assembly.DefinedTypes;

            var dic = assemblyTypes
                .Where( t => t.IsClass && t.CustomAttributes
                    .Any( attr => attr.AttributeType == typeof( LocalizedRoutesAttribute ) ) )
                .ToDictionary(
                    x => GetShortControllerName(x.Name),
                    y => y.DeclaredMethods
                        .Where(m => m.IsPublic)
                            .Select(m => m.Name));

            return dic;
        }

        private string GetShortControllerName( string fullName )
        {
            return fullName.Replace( "Controller", "" );
        }
    }
}
