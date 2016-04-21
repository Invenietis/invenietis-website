using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;

namespace CK.DB.Resource
{
    [SqlTable( "tLCID", Package = typeof( Package ) ), Versions( "1.0.1" )]
    public abstract class LCIDTable : SqlTable
    {
        void Construct( XLCIDTable xlcid )
        {
        }
    }
}
