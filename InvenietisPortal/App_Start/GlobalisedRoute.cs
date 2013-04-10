using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplication1.App_Start
{
    public class LocalizedRoute : Route
    {
        public const string CultureKey = "culture";
        
        static string CreateCultureRoute( string unGlobalisedUrl )
        {
            return string.Format( "{{" + CultureKey + "}}/{0}", unGlobalisedUrl );
        }

        public LocalizedRoute( string unGlobalisedUrl, RouteValueDictionary defaults ) :
            base( CreateCultureRoute( unGlobalisedUrl ),
                 defaults,
                 new RouteValueDictionary( new { culture = new CultureRouteConstraint() } ),
                 new GlobalisationRouteHandler() )
        {
            _actionTrans = new Dictionary<string, Dictionary<string, string>>();
            var fr = new Dictionary<string, string>();
            var en = new Dictionary<string, string>();
            _actionTrans.Add( "fr", fr );
            _actionTrans.Add( "en", en );

            fr.Add( "Contact", "contactez-nous" );
            fr.Add( "Index", "acceuil" );
            fr.Add( "Blog", "Blog" );
            fr.Add( "Legal", "mentions-legales" );
            fr.Add( "Cuke", "cuke" );

            en.Add( "Contact", "contact-us" );
            en.Add( "Index", "home" );
            en.Add( "Blog", "Blog" );
            en.Add( "Legal", "legal-terms" );
            en.Add( "Cuke", "" );
        }

        private Dictionary<string,Dictionary<string,string>> _actionTrans;

        public override RouteData GetRouteData( HttpContextBase httpContext )
        {
            RouteData rd = base.GetRouteData( httpContext );
            if( rd != null && _actionTrans.Keys.Contains( rd.Values["culture"].ToString() ) )
            {
                var keyPair = _actionTrans[rd.Values["culture"].ToString()].Where( x => x.Value.ToLower() == rd.Values["action"].ToString().ToLower() ).FirstOrDefault();
                if( keyPair.Value != null )
                {
                    rd.Values["action"] = keyPair.Key;
                }
            }
            return rd;
        }

        public override VirtualPathData GetVirtualPath( RequestContext requestContext, RouteValueDictionary values )
        {
            //var b1 = base.GetVirtualPath( requestContext, values );
            //return b1;
            RouteValueDictionary vals;
            if( values != null ) vals = new RouteValueDictionary( values );
            else vals = new RouteValueDictionary();

            string culture = "";
            //1. values
            if( values != null && values["culture"] != null )
            {
                culture = values["culture"].ToString();
            }
            //2. requestContext
            else if( requestContext.RouteData != null && requestContext.RouteData.Values["culture"] != null )
            {
                culture = requestContext.RouteData.Values["culture"].ToString();
            }
            //3. Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName
            else
            {
                culture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            }

            if( culture != string.Empty && 
                CultureManager.CultureIsSupported( culture ) && 
                values != null && 
                values["action"] != null &&
                values["action"].ToString() != "Index")
            {
                var keyPair = _actionTrans[culture].Where( x => x.Key.ToLower() == values["action"].ToString().ToLower() ).FirstOrDefault();
                if( keyPair.Value != null )
                {
                    if( vals["action"] != null ) { vals["action"] = keyPair.Value; } else { vals.Add( "action", keyPair.Value ); }
                }
            }
            var b = base.GetVirtualPath( requestContext, vals );
            return b;
        }
    }

    public class GlobalisationRouteHandler : MvcRouteHandler
    {
        string CultureValue
        {
            get
            {
                return (string)RouteDataValues[LocalizedRoute.CultureKey];
            }
        }

        RouteValueDictionary RouteDataValues { get; set; }

        protected override IHttpHandler GetHttpHandler( RequestContext requestContext )
        {
            RouteDataValues = requestContext.RouteData.Values;
            CultureManager.SetCulture( CultureValue );
            return base.GetHttpHandler( requestContext );
        }
    }

    public class CultureRouteConstraint : IRouteConstraint
    {
        public bool Match( HttpContextBase httpContext,
            Route route,
            string parameterName,
            RouteValueDictionary values,
            RouteDirection routeDirection )
        {
            if( !values.ContainsKey( parameterName ) ) return false;

            string potentialCultureName = (string)values[parameterName];

            return CultureFormatChecker.FormattedAsCulture( potentialCultureName ) && CultureManager.CultureIsSupported( potentialCultureName );
        }
    }

    public static class CultureManager
    {
        const string EnglishCultureName = "en";
        const string FrenchCultureName = "fr";

        public static CultureInfo DefaultCulture
        {
            get
            {
                return SupportedCultures[EnglishCultureName];
            }
        }

        static Dictionary<string, CultureInfo> SupportedCultures { get; set; }


        static void AddSupportedCulture( string name )
        {
            SupportedCultures.Add( name, CultureInfo.CreateSpecificCulture( name ) );
        }

        static void InitializeSupportedCultures()
        {
            SupportedCultures = new Dictionary<string, CultureInfo>();
            AddSupportedCulture( FrenchCultureName );
            AddSupportedCulture( EnglishCultureName );
        }

        static string ConvertToShortForm( string code )
        {
            return code.Substring( 0, 2 );
        }

        public static bool CultureIsSupported( string code )
        {
            if( string.IsNullOrWhiteSpace( code ) )
                return false;
            code = code.ToLowerInvariant();
            if( code.Length == 2 )
                return SupportedCultures.ContainsKey( code );
            return CultureFormatChecker.FormattedAsCulture( code ) && SupportedCultures.ContainsKey( ConvertToShortForm( code ) );
        }

        static CultureInfo GetCulture( string code )
        {
            if( !CultureIsSupported( code ) )
                return DefaultCulture;
            string shortForm = ConvertToShortForm( code ).ToLowerInvariant(); ;
            return SupportedCultures[shortForm];
        }

        public static void SetCulture( string code )
        {
            CultureInfo cultureInfo = GetCulture( code );
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        static CultureManager()
        {
            InitializeSupportedCultures();
        }
    }

    public static class CultureFormatChecker
    {
        //This matches the format xx or xx-xx 
        // where x is any alpha character, case insensitive
        //The router will determine if it is a supported language
        static Regex _cultureRegexPattern = new Regex( @"^([a-zA-Z]{2})(-[a-zA-Z]{2})?$", RegexOptions.IgnoreCase & RegexOptions.Compiled );

        public static bool FormattedAsCulture( string code )
        {
            if( string.IsNullOrWhiteSpace( code ) ) return false;

            return _cultureRegexPattern.IsMatch( code );

        }
    }
}