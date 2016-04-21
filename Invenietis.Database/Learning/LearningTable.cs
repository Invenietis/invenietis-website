using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CK.Setup;
using CK.SqlServer.Setup;

namespace Invenietis.Database.Learning
{
    [SqlTable( "tLearning", Package = typeof( Package ) ), Versions( "1.0.0" )]
    public abstract class LearningTable : SqlTable
    {
        void Construct( CK.DB.Resource.ResTable ResTable, LearningCategoryTable LearningCategoryTable )
        {

        }
    }
}
