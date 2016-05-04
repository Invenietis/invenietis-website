using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Data;
using Invenietis.Data.Entities;

namespace Invenietis.Repositories.Commands
{
    public class ProjectRepository
    {
        public ProjectRepository()
        {

        }

        public int CreateProject( Project project )
        {
            using( var db = new DataContext() )
            {
                return db.Projects.Insert( project );
            }
        }

        public bool UpdateProject( Project project )
        {
            using( var db = new DataContext() )
            {
                return db.Projects.Update( project );
            }
        }

        public bool DeleteProject( int projectId )
        {
            using( var db = new DataContext() )
            {
                return db.Projects.Delete( projectId );
            }
        }

        public int CreateCategory( ProjectCategory category )
        {
            using( var db = new DataContext() )
            {
                return db.ProjectCategories.Insert( category );
            }
        }

        public bool UpdateCategory( ProjectCategory category )
        {
            using( var db = new DataContext() )
            {
                return db.ProjectCategories.Update( category );
            }
        }

        public bool DeleteCategory( int categoryId )
        {
            using( var db = new DataContext() )
            {
                return db.ProjectCategories.Delete( categoryId );
            }
        }
    }
}
