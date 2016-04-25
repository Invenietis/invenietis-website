using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;

namespace CK.DB.Resource
{
    [SqlTable( "tResHtml", Package = typeof( Package ) ), Versions( "1.0.0" )]
    [SqlObjectItem( "vResHtml, vXLCIDResHtml" )]
    public abstract class ResHtmlTable : SqlTable
    {
        void Construct( ResTable res, LCIDTable lcid )
        {
        }

        [SqlProcedureNonQuery( "sResHtmlSet" )]
        public abstract void HtmlSet( ISqlCallContext ctx, int resId, short lcid, string value );

        [SqlProcedureNonQuery( "sResHtmlAssume" )]
        public abstract int HtmlAssume( ISqlCallContext ctx, string resName, short lcid, string value );

        [SqlProcedureNonQuery( "sResHtmlRemove" )]
        public abstract void HtmlRemove( ISqlCallContext ctx, int resId, short lcid );

    }
}
