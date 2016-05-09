using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Invenietis.Common;
using Invenietis.Common.Cultures;
using Invenietis.Data;
using Invenietis.LocalizedRoutes;
using Invenietis.LocalizedRoutes.Config;
using Invenietis.LocalizedRoutes.Mvc;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Localization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Routing.Template;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Q = Invenietis.Repositories.Queries;

namespace Invenietis.Web
{
    public class Startup
    {
        public Startup( IHostingEnvironment env )
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("routes.json")
                .AddJsonFile("config.json");

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            // Add framework services.
            services.AddMvc();

            // Localized routes provider
            var routeProvider = new LocalizedRouteProvider( Configuration.Get<LocalizedRouteConfig>() );
            var mvcAdapter = new LocalizedRoutesMvcAdapter( routeProvider, typeof(Startup).GetTypeInfo().Assembly );

            services.AddInstance( routeProvider );
            services.AddInstance( mvcAdapter );

            // Cultures provider
            var config = Configuration.Get<Config>();
            if( config == null ) throw new ArgumentNullException( "Config must be specified in config.json" );
            var cultures = config.Cultures;
            if( cultures == null ) throw new ArgumentNullException( "Cultures must be specified in config.json" );

            services.AddInstance( new CultureProvider( cultures.DefaultCulture, cultures.SupportedCultures, cultures.FallbackMap ) );

            // Repositories
            services.AddSingleton<Q.ProjectRepository>();
            services.AddSingleton<Q.LearningRepository>();
            services.AddSingleton<Q.ClientRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, LocalizedRouteProvider routeProvider, LocalizedRoutesMvcAdapter routeMvcAdapter )
        {
            loggerFactory.AddConsole( Configuration.GetSection( "Logging" ) );
            loggerFactory.AddDebug();

            if( env.IsDevelopment() ) app.UseDeveloperExceptionPage();
            else app.UseExceptionHandler( "/Home/Error" );

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseStatusCodePages();

            // DataContext provider
            var dbPath =  Configuration.Get<Config>().DatabasePath;
            if( String.IsNullOrEmpty( dbPath ) ) throw new ArgumentNullException( "DatabasePath must be specified in config.json" );
            DataContext.GetDefault = () => new DataContext( dbPath );

            // Localization
            var cultureConfig = Configuration.Get<CultureConfig>();
            ConfigureLocalization( app, cultureConfig );

            // Build localized routes
            routeProvider.SetupCultures( cultureConfig );
            routeProvider.Build();

            app.UseMvc( routes =>
            {
                // Map the localized routes for MVC controllers/actions
                routeMvcAdapter.MapLocalizedRoutes( routes );

                // Default route to choose the right localization
                routes.MapRoute( "Default", "", new { controller = "Localization", action = "Index" } );
            } );
        }

        // Change culture if url starts with '/fr' or '/en', ...
        private void ConfigureLocalization( IApplicationBuilder app, ICultureConfig cultureConfig )
        {
            RequestCulture defaultCulture = new RequestCulture( cultureConfig.DefaultCulture );

            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions();
            localizationOptions.SupportedCultures = cultureConfig.SupportedCultures.Select( x => new CultureInfo( x ) ).ToList();

            localizationOptions.RequestCultureProviders = new List<IRequestCultureProvider>()
            {
                 new UrlCultureProvider( cultureConfig )
            };

            app.UseRequestLocalization( localizationOptions, defaultCulture );
        }

        // Entry point for the application.
        public static void Main( string[] args ) => WebApplication.Run<Startup>( args );
    }
}
