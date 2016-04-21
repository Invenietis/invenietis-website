using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CK.Setup;
using CK.SqlServer.Setup;

namespace Invenietis.Database.Project
{
    [SqlTable( "tProjectCategory", Package = typeof( Package ) ), Versions( "1.0.0" )]
    public abstract class ProjectCategoryTable : SqlTable
    {
        void Construct( CK.DB.Resource.ResTable ResTable )
        {

        }
    }
}
