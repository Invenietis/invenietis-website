using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.LocalizedRoutes.Config;

namespace Invenietis.Common.Cultures
{
    public class CultureProvider
    {
        public CultureProvider( string defaultCulture, IEnumerable<string> supportedCultures, Dictionary<string, string[]> fallbackMap )
        {
            DefaultCulture = new SimpleCulture( defaultCulture, CultureInfo.GetCultureInfo( defaultCulture ).NativeName );
            SupportedCultures = supportedCultures.Select( x => new SimpleCulture( x, CultureInfo.GetCultureInfo( x ).NativeName ) );
            FallbackMap = fallbackMap;
        }

        public SimpleCulture DefaultCulture { get; }

        public IEnumerable<SimpleCulture> SupportedCultures { get; }

        public Dictionary<string, string[]> FallbackMap { get; }
    }

    public class SimpleCulture
    {
        public SimpleCulture( string id, string name )
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }

        public string Name { get; }
    }
}
