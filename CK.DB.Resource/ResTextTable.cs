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
    public abstract class ResTextTable : SqlTable
    {
        void Construct( ResTable res, LCIDTable lcid )
        {
        }

        [SqlProcedureNonQuery( "sResTextSet" )]
        public abstract void TextSet( ISqlCallContext ctx, int resId, short lcid, string value );

        [SqlProcedureNonQuery( "sResTextAssume" )]
        public abstract int TextAssume( ISqlCallContext ctx, string resName, short lcid, string value );

        [SqlProcedureNonQuery( "sResTextRemove" )]
        public abstract void TextRemove( ISqlCallContext ctx, int resId, short lcid );

    }
}
