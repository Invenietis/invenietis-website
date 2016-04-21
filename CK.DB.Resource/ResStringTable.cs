using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;

namespace CK.DB.Resource
{
    [SqlTable( "tResString", Package = typeof( Package ) ), Versions( "1.0.0" )]
    [SqlObjectItem( "vResString, vXLCIDResString" )]
    [SqlObjectItem( "sResStringSet, sResStringAssume, sResStringRemove" )]
    public abstract class ResStringTable : SqlTable
    {
        void Construct( ResTable res, LCIDTable lcid )
        {
        }
    }
}
