using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CK.Setup;
using CK.SqlServer.Setup;

namespace Invenietis.Database.Learning
{
    /// <summary>
    /// Learning package.
    /// </summary>
    [SqlPackage( FullName = "LearningPackage",
                 Schema = "Inv",
                 Database = typeof( SqlDefaultDatabase ),
                 ResourceType = typeof( Package ),
                 ResourcePath = "~Invenietis.Database.Learning.Res" )]
    [Versions( "1.0.0" )]
    public class Package : SqlPackage
    {
        void Construct()
        {

        }
    }
}
