using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Data;
using Invenietis.Data.Entities;

namespace Invenietis.Repositories.Queries
{
    public class ProjectRepository
    {
        public ProjectRepository()
        {

        }

        public IEnumerable<Project> GetProjectsByCategory( int categoryId )
        {
            using( var db = new DataContext() )
            {
                var projects = db.Projects.Find( x => x.Category.Id == categoryId ).ToArray();
                PopulateProjects( db, projects );

                return projects;
            }
        }

        public IEnumerable<Project> GetProjectsByClient( int clientId )
        {
            using( var db = new DataContext() )
            {
                var projects = db.Projects.Find( x => x.Client.Id == clientId ).ToArray();
                PopulateProjects( db, projects );

                return projects;
            }
        }

        private void PopulateProjects( DataContext db, params Project[] projects )
        {
            foreach( var p in projects )
            {
                p.Category.Fetch( db.Connection );
                p.Client.Fetch( db.Connection );
            }
        }

        public Project GetProjectById( int projectId )
        {
            using( var db = new DataContext() )
            {
                var project = db.Projects.FindById( projectId );
                if( project != null ) project.Category.Fetch( db.Connection );

                return project;
            }
        }

        public IEnumerable<ProjectCategory> GetProjectCategories()
        {
            using( var db = new DataContext() )
            {
                return db.ProjectCategories.FindAll().ToList();
            }
        }

        public ProjectCategory GetProjectCategoryById( int categoryId )
        {
            using( var db = new DataContext() )
            {
                return db.ProjectCategories.FindById( categoryId );
            }
        }
    }
}
