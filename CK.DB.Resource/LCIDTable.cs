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

        [SqlProcedureNonQuery( "sResCultureRegister" )]
        public abstract void CultureRegister( ISqlCallContext ctx, short lcid, string name, string englishName, string nativeName, short parentLCID );

        [SqlProcedureNonQuery( "sResCultureDestroy" )]
        public abstract void CultureDestroy( ISqlCallContext ctx, short lcid );

        public void RegisterCulture( ISqlCallContext ctx, CultureInfo c )
        {
            if( c == null ) throw new ArgumentNullException();
            if( c.LCID == 127 ) return;
            if( c.Parent.LCID != 127 ) RegisterCulture( ctx, c.Parent );
            CultureRegister( ctx, (short)c.LCID, c.Name, c.EnglishName, c.NativeName, (short)c.Parent.LCID );
        }
    }
}
