﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;

namespace CK.DB.Resource
{
    [SqlTable( "tXLCIDMap", Package = typeof( Package ) ), Versions( "1.0.0" )]
    [SqlObjectItem( "vXLCID, vLCID" )]
    public abstract class XLCIDMapTable : SqlTable
    {
        void Construct( XLCIDTable xlcid, LCIDTable lcid )
        {
        }
    }
}
