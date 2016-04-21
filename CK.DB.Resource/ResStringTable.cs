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
    public abstract class ResStringTable : SqlTable
    {
        void Construct( ResTable res, LCIDTable lcid )
        {
        }

        [SqlProcedureNonQuery( "sResStringSet" )]
        public abstract void StringSet( int resId, short lcid, string value );

        [SqlProcedureNonQuery( "sResStringAssume" )]
        public abstract int StringAssume( string resName, short lcid, string value );

        [SqlProcedureNonQuery( "sResStringRemove" )]
        public abstract void StringRemove( int resId, short lcid );

    }
}
