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
    public class LearningRepository : BaseRepository
    {
        public LearningRepository( CultureProvider c )
            : base( c )
        {

        }

        public int CreateLearning()
        {
            using( var db = DataContext.GetDefault() )
            {
                var learning = new Learning();
                foreach( var c in CultureProvider.SupportedCultures ) learning.Cultures.Add( c.Id, null );

                return db.Learnings.Insert( learning );
            }
        }

        public bool UpdateLearning( Learning learning )
        {
            using( var db = DataContext.GetDefault() )
            {
                learning.Category = learning.CategoryId > 0 ? new DbRef<LearningCategory>( DataContext.LearningCategoriesCollection, learning.CategoryId ) : null;
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

        public int CreateCategory()
        {
            using( var db = DataContext.GetDefault() )
            {
                var category = new LearningCategory();
                foreach( var c in CultureProvider.SupportedCultures ) category.Cultures.Add( c.Id, null );

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
