using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.LocalizedRoutes.Config;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Localization;

namespace Invenietis.LocalizedRoutes
{
    /// <summary>
    /// Determines the culture information for a request via the value of the start of a url.
    /// </summary>
    public class UrlCultureProvider : RequestCultureProvider
    {
        public UrlCultureProvider( CultureConfig cultureConfig )
        {
            if( cultureConfig == null ) throw new ArgumentNullException( nameof( cultureConfig ) );

            CultureConfiguration = cultureConfig;
        }

        /// <summary>
        /// The configuration used to manage cultures and fallbacks
        /// </summary>
        public CultureConfig CultureConfiguration { get; set; }

        public override Task<ProviderCultureResult> DetermineProviderCultureResult( HttpContext httpContext )
        {
            if( httpContext == null ) throw new ArgumentNullException( nameof( httpContext ) );

            var url = httpContext.Request.Path.Value;
            var pathLength = url.Length;

            // Example: /fr/...
            if( pathLength >= 3 )
            {
                if( url.Length >= 4 )
                {
                    // If url looks like /fra, /something : it's not supported
                    if( url[3] != '/' ) return Task.FromResult( new ProviderCultureResult( CultureConfiguration.DefaultCulture ) );
                }

                // Extract the language name
                var startPath = url.Substring( 1, 2 );
                var culture =  CultureConfiguration.SupportedCultures.SingleOrDefault( x => x == startPath ) ?? ResolveFallback(startPath);

                return Task.FromResult( new ProviderCultureResult( culture ) );
            }

            return Task.FromResult( new ProviderCultureResult( CultureConfiguration.DefaultCulture ) );
        }

        private string ResolveFallback( string culture )
        {
            string[] fallbacks;
            var fallbacksExist = CultureConfiguration.FallbackMap.TryGetValue( culture, out fallbacks );

            if( !fallbacksExist ) fallbacks = new[] { CultureConfiguration.DefaultCulture };

            return fallbacks.First();
        }
    }
}
