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
    public class LearningRepository
    {
        public LearningRepository()
        {

        }

        public PaginatedResult<Learning> GetLearnings( LearningFilter lFilter, OrderFilter oFilter, PaginationInfo pInfo )
        {
            using( var db = DataContext.GetDefault() )
            {
                var total = db.Learnings.Count();

                var qOrder = oFilter.OrderDesc ? -1 : 1;
                Query q = !String.IsNullOrEmpty(oFilter.OrderBy) ? Query.All( oFilter.OrderBy, qOrder ) : Query.All( qOrder );

                if( lFilter.CategoryId > 0 ) q = Query.And( q, Query.EQ( "CategoryId", lFilter.CategoryId ) );
                if( lFilter.Published.HasValue ) q = Query.And( q, Query.EQ( "Published", lFilter.Published.Value ) );

                var learnings = db.Learnings.Find(q, pInfo.Page * pInfo.PerPage, pInfo.PerPage).ToArray();
                PopulateLearnings( db, learnings );

                return new PaginatedResult<Learning>( pInfo, learnings, total );
            }
        }

        private void PopulateLearnings( DataContext db, params Learning[] learnings )
        {
            foreach( var p in learnings )
            {
                p.Category.Fetch( db.Connection );
            }
        }

        public Learning GetLearningById( int learningId )
        {
            using( var db = DataContext.GetDefault() )
            {
                var learning = db.Learnings.FindById( learningId );
                if( learning != null ) learning.Category.Fetch( db.Connection );

                return learning;
            }
        }

        public IEnumerable<LearningCategory> GetLearningCategories()
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.LearningCategories.FindAll().ToArray();
            }
        }

        public LearningCategory GetLearningCategoryById( int categoryId )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.LearningCategories.FindById( categoryId );
            }
        }
    }
}
