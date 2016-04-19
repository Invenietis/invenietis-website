using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.TagHelpers;

namespace Invenietis.LocalizedRoutes
{
    [HtmlTargetElement( "a", Attributes = CultureAttributeName )]
    public class LocalizedActionLinkTagHelper : TagHelper
    {
        LocalizedRouteProvider _routeProvider;

        public LocalizedActionLinkTagHelper( LocalizedRouteProvider routeProvider )
            : base()
        {
            if( routeProvider == null ) throw new ArgumentNullException( nameof( routeProvider ) );

            _routeProvider = routeProvider;
        }

        private const string CultureAttributeName = "asp-culture";

        /// <summary>
        /// The culture attribute
        /// </summary>        
        [HtmlAttributeName( CultureAttributeName )]
        public string Culture { get; set; }

        public override Task ProcessAsync( TagHelperContext context, TagHelperOutput output )
        {
            if( string.IsNullOrEmpty( Culture ) ) Culture = CultureInfo.CurrentCulture.Name;

            string localizedUrl;
            string routeId = context.AllAttributes["asp-route-id"]?.Value as string;
            string routeParams = context.AllAttributes["asp-params"]?.Value?.ToString() as string;

            if( routeId == null ) throw new ArgumentNullException( nameof( routeId ) );

            localizedUrl = _routeProvider.GetLocalizedLink( routeId, Culture, ParamsToDictionary( routeParams ) );
            
            output.Attributes["href"] = localizedUrl;

            return Task.FromResult( 0 );
        }

        // Params must be like : "id: 1, name: toto"
        private Dictionary<string, string> ParamsToDictionary( string routeParams )
        {
            if( routeParams == null ) return null;

            var dic = new Dictionary<string, string>();
            var paramsList = routeParams.Split(',');

            foreach( var p in paramsList )
            {
                var nameValue = p.Split(':').Select(x => x.Trim()).ToArray();
                if( nameValue.Length != 2 ) throw new ArgumentException( $"Route parameter wrong format: {p}" );

                var name = nameValue[0];
                var value = nameValue[1];

                dic.Add( name, value );
            }

            return dic;
        }
    }
}
