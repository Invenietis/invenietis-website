﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CK.Setup;
using CK.SqlServer.Setup;

namespace Invenietis.Database
{
    /// <summary>
    /// Main Inv package.
    /// </summary>
    [SqlPackage( FullName = "InvPackage",
                 Schema = "Inv",
                 Database = typeof( SqlDefaultDatabase ),
                 ResourceType = typeof( Package ),
                 ResourcePath = "~Invenietis.Database.Res" )]
    [Versions( "1.0.0" )]
    public class Package : SqlPackage
    {
        void Construct()
        {

        }
    }
}
