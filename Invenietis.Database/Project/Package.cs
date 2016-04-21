using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CK.Setup;
using CK.SqlServer.Setup;

namespace Invenietis.Database.Project
{
    /// <summary>
    /// Project package.
    /// </summary>
    [SqlPackage( FullName = "ProjectPackage",
                 Schema = "Inv",
                 Database = typeof( SqlDefaultDatabase ),
                 ResourceType = typeof( Package ),
                 ResourcePath = "~Invenietis.Database.Project.Res" )]
    [Versions( "1.0.0" )]
    public class Package : SqlPackage
    {
        void Construct( CK.DB.Resource.Package ResPackage )
        {

        }
    }
}
