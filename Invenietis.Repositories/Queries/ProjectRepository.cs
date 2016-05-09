using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common;
using Invenietis.Data;
using Invenietis.Data.Entities;
using Invenietis.Repositories.Queries.Filters;
using LiteDB;

namespace Invenietis.Repositories.Queries
{
    public class ProjectRepository
    {
        public ProjectRepository()
        {

        }

        public PaginatedResult<Project> GetProjects( ProjectFilter pFilter, OrderFilter oFilter, PaginationInfo pInfo )
        {
            using( var db = DataContext.GetDefault() )
            {
                var total = db.Learnings.Count();

                var qOrder = oFilter.OrderDesc ? -1 : 1;
                Query q = !String.IsNullOrEmpty(oFilter.OrderBy) ? Query.All( oFilter.OrderBy, qOrder ) : Query.All( qOrder );

                if( pFilter.CategoryId > 0 ) q = Query.And( q, Query.EQ( "CategoryId", pFilter.CategoryId ) );
                if( pFilter.ClientId > 0 ) q = Query.And( q, Query.EQ( "ClientId", pFilter.ClientId ) );
                if( pFilter.Published.HasValue ) q = Query.And( q, Query.EQ( "Published", pFilter.Published.Value ) );

                var projects = db.Projects.Find(q, pInfo.Page * pInfo.PerPage, pInfo.PerPage).ToArray();
                PopulateProjects( db, projects );

                return new PaginatedResult<Project>( pInfo, projects, total );
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
            using( var db = DataContext.GetDefault() )
            {
                var project = db.Projects.FindById( projectId );
                if( project != null ) project.Category.Fetch( db.Connection );

                return project;
            }
        }

        public IEnumerable<ProjectCategory> GetProjectCategories()
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.ProjectCategories.FindAll().ToArray();
            }
        }

        public ProjectCategory GetProjectCategoryById( int categoryId )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.ProjectCategories.FindById( categoryId );
            }
        }
    }
}
