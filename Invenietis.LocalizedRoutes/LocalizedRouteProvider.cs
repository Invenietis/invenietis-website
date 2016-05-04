using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Invenietis.LocalizedRoutes.Config;

namespace Invenietis.LocalizedRoutes
{
    /// <summary>
    /// Build localized routes from a specified configuration. 
    /// Routes are composed with fragments, support inheritance and cultural fallbacks.
    /// In order to use it, you must configure it by calling SetupCultures, then Build.
    /// </summary>
    public class LocalizedRouteProvider
    {
        List<LocalizedRoute> _routes;
        ILocalizedRouteConfig _config;
        ICultureConfig _cultureConfig;
        Regex _paramsRegex = new Regex("{(?<routeParam>[a-z0-9]*)(?::[a-z0-9]*)*}", RegexOptions.Compiled);

        /// <summary>
        /// Initialize a new instance of LocalizedRouteProvider
        /// </summary>
        /// <param name="config">The configuration used to build routes</param>
        public LocalizedRouteProvider( ILocalizedRouteConfig config )
        {
            if( config == null ) throw new InvalidOperationException( nameof( config ) );

            _config = config;
            _routes = new List<LocalizedRoute>();
        }

        /// <summary>
        /// Trigger when a new route is added to this instance
        /// </summary>
        public event EventHandler<LocalizedRouteEventArgs> RouteAdded;

        private void OnRouteAdded( ILocalizedRoute localizedRoute )
        {
            var h = RouteAdded;

            if( h != null ) h( this, new LocalizedRouteEventArgs( localizedRoute ) );
        }

        /// <summary>
        /// Trigger when a route was removed from this instance
        /// </summary>
        public event EventHandler<LocalizedRouteEventArgs> RouteRemoved;

        private void OnRouteRemoved( ILocalizedRoute localizedRoute )
        {
            var h = RouteRemoved;

            if( h != null ) h( this, new LocalizedRouteEventArgs( localizedRoute ) );
        }

        /// <summary>
        /// The configuration used to manage cultures and fallbacks
        /// </summary>
        public ICultureConfig CultureConfiguration { get { return _cultureConfig; } }

        /// <summary>
        /// The routes built from the specified configuration
        /// </summary>
        public IEnumerable<ILocalizedRoute> Routes { get { return _routes; } }

        /// <summary>
        /// Setup default culture, supported cultures, and fallbacks
        /// </summary>
        /// <param name="config">The specified configuration</param>
        public void SetupCultures( ICultureConfig config )
        {
            if( config == null ) throw new InvalidOperationException( nameof( config ) );

            _cultureConfig = config;
        }

        /// <summary>
        /// Build the localized routes from the specified configuration
        /// </summary>
        public void Build()
        {
            if( _routes.Count > 0 ) throw new InvalidOperationException( $"Build was already done ({_routes.Count} existing routes)." );

            foreach( var rc in _config.Cultures )
            {
                var culture = rc.Culture;
                var defaultRouteId = rc.DefaultRouteId;

                foreach( var rf in rc.RouteFragments )
                {
                    if( rf.IsAbstract ) continue;

                    var template = BuildTemplate( rf.Id, rc );

                    if( _routes.Any( x => x.Id == rf.Id && x.Culture == rc.Culture ) ) throw new InvalidOperationException( $"Duplicate entry for route {rf.Id}, culture {rc.Culture}" );

                    _routes.Add( new LocalizedRoute()
                    {
                        Culture = rc.Culture,
                        Id = rf.Id,
                        Name = BuildRouteName( culture, rf.Id ),
                        Template = template,
                        IsDefault = defaultRouteId == rf.Id
                    } );
                }
            }
        }

        /// <summary>
        /// Add a new route in the specified culture. 
        /// Triggers a <see cref="RouteAdded"/> event.
        /// </summary>
        /// <param name="culture">The culture of the route fragment</param>
        /// <param name="routeFragment">The route fragment to add</param>
        /// <returns>The resulting localized route, or null if the fragment is abstract</returns>
        public ILocalizedRoute AddFragment( string culture, IRouteFragment routeFragment )
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the specified route.
        /// Triggers a <see cref="RouteRemoved"/> event.
        /// </summary>
        /// <param name="route">The route to remove.</param>
        public void RemoveRoute( ILocalizedRoute route )
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the specified route fragment.
        /// </summary>
        /// <param name="culture">The culture of the fragment</param>
        /// <param name="fragmentId">The id of the fragment</param>
        public void RemoveFragment( string culture, string fragmentId )
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate a culture-aware link from specified route id, culture, and route parameters. 
        /// If the culture is not specified, <see cref="CultureInfo.CurrentCulture"/> is used.
        /// Route parameters are optional and must be used only if the specified route supports it.
        /// </summary>
        /// <param name="routeId">The id of the route to use to generate the link.</param>
        /// <param name="culture">The desired culture for the link. If the route doesn't exist for this culture, a fallback is applied as specified in the <see cref="CultureConfiguration"/></param>.
        /// <param name="routeParamsValues">The parameters values for the route, if the route supports it.</param>
        /// <returns></returns>
        public string GetLocalizedLink( string routeId, string culture = null, Dictionary<string, string> routeParamsValues = null )
        {
            culture = culture ?? CultureInfo.CurrentCulture.Name;

            var route = GetLocalizedRoute(routeId, culture);

            return GetParameterizedUrl( route, routeParamsValues );
        }

        /// <summary>
        /// Find a localized route from the specified routeId, and culture.
        /// If the culture is not specified, <see cref="CultureInfo.CurrentCulture"/> is used.
        /// </summary>
        /// <param name="routeId">The id of the route to find.</param>
        /// <param name="culture">The culture of the route. If the route doesn't exist for this culture, a fallback is applied as specified in the <see cref="CultureConfiguration"/></param>
        /// <returns>The best ILocalizedRoute matching the specified conditions</returns>
        public ILocalizedRoute GetLocalizedRoute( string routeId, string culture = null )
        {
            culture = culture ?? CultureInfo.CurrentCulture.Name;

            ILocalizedRoute route = _routes.SingleOrDefault(x => x.Culture == culture && x.Id == routeId) ?? ResolveFallback( routeId, culture );

            return route;
        }

        /// <summary>
        /// Find a localized route from given predicate, and culture.
        /// If the culture is not specified, <see cref="CultureInfo.CurrentCulture"/> is used.
        /// </summary>
        /// <param name="predicate">Filter that must match only and at least one element.</param>
        /// <param name="culture">The culture of the route. If the route doesn't exist for this culture, a fallback is applied as specified in the <see cref="CultureConfiguration"/>.FallbackMap</param>
        /// <returns>The best ILocalizedRoute matching the specified conditions</returns>
        public ILocalizedRoute GetLocalizedRoute( Func<ILocalizedRoute, bool> predicate, string culture = null )
        {
            culture = culture ?? CultureInfo.CurrentCulture.Name;

            ILocalizedRoute route = _routes.SingleOrDefault( x => x.Culture == culture && predicate(x)) ?? ResolveFallback( predicate, culture );

            if( route == null ) throw new InvalidOperationException( $"Route for culture '{culture}' doesn't exist, and no fallback was found." );

            return route;
        }

        private ILocalizedRoute ResolveFallback( Func<ILocalizedRoute, bool> predicate, string wantedCulture )
        {
            string[] fallbacks;
            var fallbacksExist = _cultureConfig.FallbackMap.TryGetValue( wantedCulture, out fallbacks );

            if( !fallbacksExist ) fallbacks = new[] { _cultureConfig.DefaultCulture };

            var matchingCulture = fallbacks.FirstOrDefault(x => _routes.Any( y => y.Culture == x && predicate(y) ));

            if( matchingCulture == null ) return null;

            return _routes.Single( x => x.Culture == matchingCulture && predicate( x ) );
        }

        private ILocalizedRoute ResolveFallback( string routeId, string wantedCulture )
        {
            var fallback = ResolveFallback( x => x.Id == routeId, wantedCulture );

            if( fallback == null ) throw new InvalidOperationException( $"Route {routeId} for culture '{wantedCulture}' doesn't exist, and no fallback was found." );

            return fallback;
        }

        private string BuildRouteName( string culture, string id )
        {
            return $"{culture}.{id}";
        }

        private string BuildTemplate( string id, IRouteCulture rc )
        {
            var sb = new StringBuilder();
            sb.Append( rc.Culture );
            sb.Append( '/' );

            var idParts = id.Split('.');

            for( var i = 0; i < idParts.Length; i++ )
            {
                var subPartId = GetSubPartId(idParts, i + 1);

                var subFragment = FindFragmentById(rc, subPartId);
                if( subFragment == null ) throw new InvalidOperationException( $"Trying to build route {id}, but sub-route {subPartId} doesn't exist." );

                sb.Append( subFragment.Value );

                // We don't want doubled slashes, or end slash
                if( !String.IsNullOrWhiteSpace( subFragment.Value ) && i < idParts.Length - 1 ) sb.Append( '/' );
            }

            return sb.ToString();
        }

        private string GetSubPartId( string[] idParts, int depth )
        {
            return idParts.Take( depth ).Aggregate( ( x, y ) => x + "." + y );
        }

        private IRouteFragment FindFragmentById( IRouteCulture rc, string id )
        {
            return rc.RouteFragments.SingleOrDefault( x => x.Id == id );
        }

        private string[] GetIdParts( string id )
        {
            return id.Split( '.' );
        }

        private string GetParameterizedUrl( ILocalizedRoute route, Dictionary<string, string> routeParamsValues = null )
        {
            var sb = new StringBuilder();
            sb.Append( "/" ).Append( route.Template );

            var urlTemplate = sb.ToString();

            // Route parameters handling
            var paramsGroups = _paramsRegex.Matches(urlTemplate);

            // No given parameter but route expect it
            if( (routeParamsValues == null || routeParamsValues.Count == 0) && paramsGroups.Count > 0 )
                throw new InvalidOperationException( $"Route {route.Name}: parameters expected" );

            if( routeParamsValues != null && routeParamsValues.Count > 0 )
            {
                // Given parameter not supported by the route
                if( paramsGroups.Count == 0 ) throw new InvalidOperationException( $"Route {route.Name} doesn't support parameters" );

                foreach( Match m in paramsGroups )
                {
                    string givenParam;
                    var rParam = m.Groups["routeParam"].Value;

                    if( !routeParamsValues.TryGetValue( rParam, out givenParam ) )
                        throw new ArgumentException( $"Route {route.Name} missing argument: {m.Value}" );

                    sb.Replace( m.Value, givenParam );
                }

            }

            return sb.ToString();
        }
    }
}
