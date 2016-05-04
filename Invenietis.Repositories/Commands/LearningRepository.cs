using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Data;
using Invenietis.Data.Entities;

namespace Invenietis.Repositories.Commands
{
    public class LearningRepository
    {
        public LearningRepository()
        {

        }

        public int CreateLearning( Learning learning )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.Learnings.Insert( learning );
            }
        }

        public bool UpdateLearning( Learning learning )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.Learnings.Update( learning );
            }
        }

        public bool DeleteLearning( int learningId )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.Learnings.Delete( learningId );
            }
        }

        public int CreateCategory( LearningCategory category )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.LearningCategories.Insert( category );
            }
        }

        public bool UpdateCategory( LearningCategory category )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.LearningCategories.Update( category );
            }
        }

        public bool DeleteCategory( int categoryId )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.LearningCategories.Delete( categoryId );
            }
        }
    }
}
