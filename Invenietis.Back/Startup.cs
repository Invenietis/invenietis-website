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
                .AddJsonFile("config.json")
                .AddJsonFile("cultures.json");

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
            var cultureProvider = Configuration.Get<CultureProvider>();
            services.AddInstance( cultureProvider );

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
            var dbPath =  Configuration.Get( "DatabasePath" );
            if( String.IsNullOrEmpty( dbPath ) ) throw new ArgumentNullException( "DatabasePath must be specified in config.json" );
            DataContext.GetDefault = () => new DataContext( dbPath );

            // Build routes
            app.UseMvc( routes =>
            {
                routes.MapRoute( name: "default",
                    template: "{controller}/{action}",
                    defaults: new { controller = "Home", action = "Index" } );
            } );
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
