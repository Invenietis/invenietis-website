using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common.Cultures;
using Invenietis.Data;
using Invenietis.Data.Entities;
using LiteDB;

namespace Invenietis.Repositories.Commands
{
    public class ProjectRepository : BaseRepository
    {
        public ProjectRepository( CultureProvider c )
            : base( c )
        {

        }

        public int CreateProject()
        {
            using( var db = DataContext.GetDefault() )
            {
                var project = new Project();
                foreach( var c in CultureProvider.SupportedCultures ) project.Cultures.Add( c.Id, new CulturedProject() );

                return db.Projects.Insert( project );
            }
        }

        public bool UpdateProject( Project project )
        {
            using( var db = DataContext.GetDefault() )
            {
                project.Category = project.CategoryId > 0 ? new DbRef<ProjectCategory>( DataContext.LearningCategoriesCollection, project.CategoryId ) : null;
                project.Client = project.ClientId > 0 ? new DbRef<Client>( DataContext.ClientsCollection, project.ClientId ) : null;

                return db.Projects.Update( project );
            }
        }

        public bool DeleteProject( int projectId )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.Projects.Delete( projectId );
            }
        }

        public int CreateCategory()
        {
            using( var db = DataContext.GetDefault() )
            {
                var category = new ProjectCategory();
                foreach( var c in CultureProvider.SupportedCultures ) category.Cultures.Add( c.Id, String.Empty );

                return db.ProjectCategories.Insert( category );
            }
        }

        public bool UpdateCategory( ProjectCategory category )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.ProjectCategories.Update( category );
            }
        }

        public bool DeleteCategory( int categoryId )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.ProjectCategories.Delete( categoryId );
            }
        }
    }
}
