using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;

namespace CK.DB.Resource
{
    [SqlTable( "tResText", Package = typeof( Package ) ), Versions( "1.0.0" )]
    [SqlObjectItem( "vResText, vXLCIDResText" )]
    [SqlObjectItem( "sResTextSet, sResTextAssume, sResTextRemove" )]
    public abstract class ResTextTable : SqlTable
    {
        void Construct( ResTable res, LCIDTable lcid )
        {
        }
    }
}
