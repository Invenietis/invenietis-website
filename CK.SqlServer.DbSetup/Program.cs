using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CK.Setup;
using CK.Core;
using System.IO;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;

namespace CK.SqlServer.DbSetup
{
    public class Program
    {
        private readonly IApplicationEnvironment _appEnv;
        private readonly ILibraryManager _libMgr;

        public Program( ILibraryManager libraryManager, IApplicationEnvironment appEnv )
        {
            _libMgr = libraryManager;
            _appEnv = appEnv;
        }

        public void Main( string[] args )
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(_appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile("dbSetup.json", true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            var config = builder.Build().GetSection("CK.SqlServer.Config");

            string assembliesToSetupString = config["AssembliesToSetup"];
            string connectionString = config["ConnectionString"];
            string dynamicAssemblyName = config["DynamicAssemblyName"];
            string directory = config["Directory"] ?? _appEnv.ApplicationBasePath;

            if( String.IsNullOrWhiteSpace( connectionString ) ) { throw new InvalidOperationException( "CK.SqlServer.Config:ConnectionString configuration must be set." ); }
            if( String.IsNullOrWhiteSpace( dynamicAssemblyName ) ) { throw new InvalidOperationException( "CK.SqlServer.Config:DynamicAssemblyName configuration must be set." ); }
            if( String.IsNullOrWhiteSpace( assembliesToSetupString ) ) { throw new InvalidOperationException( "CK.SqlServer.Config:AssembliesToSetup configuration must be set." ); }

            string[] assembliesToSetup = config["AssembliesToSetup"].Split(',').Select( s => s.Trim() ).ToArray();

            if( assembliesToSetup.Length == 0 ) { throw new InvalidOperationException( "CK.SqlServer.Config:AssembliesToSetup configuration must have assemblies to set up." ); }

            ActivityMonitor m = new ActivityMonitor();
            m.Output.RegisterClient( new ActivityMonitorConsoleClient() );

            m.Info().Send( $"AssembliesToSetup: {String.Join( ", ", assembliesToSetup ) }" );
            m.Info().Send( $"ConnectionString: {connectionString}" );
            m.Info().Send( $"DynamicAssemblyName: {dynamicAssemblyName}" );
            m.Info().Send( $"Directory: {directory}" );

            SetupEngineConfiguration setupConfig = CreateConfig(assembliesToSetup, connectionString, dynamicAssemblyName, directory);

            IStObjMap resultMap = RunDBSetup( m, setupConfig );
        }

        SetupEngineConfiguration CreateConfig( IEnumerable<string> assembliesToSetup, string connectionString, string dynamicAssemblyName, string directory )
        {
            SetupEngineConfiguration _config = new SetupEngineConfiguration();
            _config.StObjEngineConfiguration.BuildAndRegisterConfiguration.UseIndependentAppDomain = false;
            foreach( var a in assembliesToSetup )
            {
                _config.StObjEngineConfiguration.BuildAndRegisterConfiguration.Assemblies.DiscoverAssemblyNames.Add( a );
            }

            _config.StObjEngineConfiguration.FinalAssemblyConfiguration.Directory = directory;
            _config.StObjEngineConfiguration.FinalAssemblyConfiguration.AssemblyName = dynamicAssemblyName;


            var c = new SqlSetupAspectConfiguration
            {
                DefaultDatabaseConnectionString = connectionString,
                IgnoreMissingDependencyIsError = true // Set to true while we don't have SqlFragment support.
            };

            _config.Aspects.Add( c );

            return _config;
        }

        public static IStObjMap RunDBSetup( IActivityMonitor Monitor, SetupEngineConfiguration Config, bool traceStObjGraphOrdering = false, bool traceSetupGraphOrdering = false )
        {
            using( Monitor.OpenTrace().Send( "Running Setup" ) )
            {
                Monitor.Trace().Send( ".Net Core Assembly (string assembly): " + typeof( string ).Assembly.Location );
                try
                {
                    Config.StObjEngineConfiguration.TraceDependencySorterInput = traceStObjGraphOrdering;
                    Config.StObjEngineConfiguration.TraceDependencySorterOutput = traceStObjGraphOrdering;
                    Config.TraceDependencySorterInput = traceSetupGraphOrdering;
                    Config.TraceDependencySorterOutput = traceSetupGraphOrdering;
                    using( var r = StObjContextRoot.Build( Config, null, Monitor ) )
                    {
                        string directory = Config.StObjEngineConfiguration.FinalAssemblyConfiguration.Directory;
                        string generatedDllName = $"{Config.StObjEngineConfiguration.FinalAssemblyConfiguration.AssemblyName}.dll";
                        string path = Path.Combine( directory, generatedDllName );

                        Debug.Assert( File.Exists( path ) );

                        Assembly generatedAssembly = Assembly.LoadFile(path);

                        IStObjMap map = StObjContextRoot.Load(generatedAssembly, StObjContextRoot.DefaultStObjRuntimeBuilder, Monitor);
                        return map;
                    }
                }
                catch( Exception ex )
                {
                    Monitor.Error().Send( ex );
                    throw;
                }
            }
        }
    }
}
