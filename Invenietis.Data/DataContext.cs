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

        public const string ProjectsCollection = "Projects";
        public LiteCollection<Project> Projects { get { return _connection.GetCollection<Project>( ProjectsCollection ); } }

        public const string ProjectCategoriesCollection = "ProjectCategories";
        public LiteCollection<ProjectCategory> ProjectCategories { get { return _connection.GetCollection<ProjectCategory>( ProjectCategoriesCollection ); } }

        public const string LearningsCollection = "Learnings";
        public LiteCollection<Learning> Learnings { get { return _connection.GetCollection<Learning>( LearningsCollection ); } }

        public const string LearningCategoriesCollection = "LearningCategories";
        public LiteCollection<LearningCategory> LearningCategories { get { return _connection.GetCollection<LearningCategory>( LearningCategoriesCollection ); } }

        public const string ClientsCollection = "Clients";
        public LiteCollection<Client> Clients { get { return _connection.GetCollection<Client>( ClientsCollection ); } }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
