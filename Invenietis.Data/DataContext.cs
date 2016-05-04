using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Data.Entities;
using LiteDB;

namespace Invenietis.Data
{
    public class DataContext : IDisposable
    {
        private LiteDatabase _connection;

        public static string DatabaseFolder { get; set; }

        public DataContext( string databaseName )
        {
            _connection = new LiteDatabase( databaseName );
        }

        public static Func<DataContext> GetDefault { get; set; }

        public LiteDatabase Connection { get { return _connection; } }

        public LiteCollection<Project> Projects { get { return _connection.GetCollection<Project>( "projects" ); } }
        public LiteCollection<ProjectCategory> ProjectCategories { get { return _connection.GetCollection<ProjectCategory>( "projectCategories" ); } }

        public LiteCollection<Learning> Learnings { get { return _connection.GetCollection<Learning>( "learnings" ); } }
        public LiteCollection<LearningCategory> LearningCategories { get { return _connection.GetCollection<LearningCategory>( "learningCategories" ); } }

        public LiteCollection<Client> Clients { get { return _connection.GetCollection<Client>( "clients" ); } }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
