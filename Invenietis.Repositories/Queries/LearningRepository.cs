using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Data;
using Invenietis.Data.Entities;

namespace Invenietis.Repositories.Queries
{
    public class LearningRepository
    {
        public LearningRepository()
        {

        }

        public IEnumerable<Learning> GetLearningsByCategory( int categoryId )
        {
            using( var db = DataContext.GetDefault() )
            {
                var learnings = db.Learnings.Find( x => x.Category.Id == categoryId ).ToArray();
                PopulateLearnings( db, learnings );

                return learnings;
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
                return db.LearningCategories.FindAll().ToList();
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
