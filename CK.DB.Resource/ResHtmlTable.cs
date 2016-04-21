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
    [SqlObjectItem( "sResHtmlSet, sResHtmlAssume, sResHtmlRemove" )]
    public abstract class ResHtmlTable : SqlTable
    {
        void Construct( ResTable res, LCIDTable lcid )
        {
        }
    }
}
