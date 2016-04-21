using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;

namespace CK.DB.Resource
{
    [SqlTable( "tXLCID", Package = typeof( Package ) ), Versions( "1.0.0" )]
    public abstract class XLCIDTable : SqlTable
    {
    }
}
