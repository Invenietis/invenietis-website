using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common.Cultures;
using Invenietis.Data;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Q = Invenietis.Repositories.Queries;
using C = Invenietis.Repositories.Commands;
using Invenietis.Common;

namespace Invenietis.Back
{
    public class Startup
    {
        public Startup( IHostingEnvironment env )
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
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

            // Cultures provider
            var config = Configuration.Get<Config>();
            if( config == null ) throw new ArgumentNullException("Config must be specified in config.json");
            var cultures = config.Cultures;
            if( cultures == null ) throw new ArgumentNullException( "Cultures must be specified in config.json" );

            services.AddInstance( new CultureProvider(cultures.DefaultCulture, cultures.SupportedCultures, cultures.FallbackMap) );

            // Repositories
            services.AddSingleton<Q.ProjectRepository>();
            services.AddSingleton<Q.LearningRepository>();
            services.AddSingleton<Q.ClientRepository>();

            services.AddSingleton<C.ProjectRepository>();
            services.AddSingleton<C.LearningRepository>();
            services.AddSingleton<C.ClientRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
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

            // Build routes
            app.UseMvc( routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}" );

                routes.MapRoute(
                    "html5",
                    "{*all}",
                    new { controller = "Home", action = "Index" } );
            } );
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
