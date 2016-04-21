using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CK.SqlServer.Setup;
using CK.Setup;

namespace CK.DB.Resource
{

    [SqlPackage( FullName="ResPackage", Schema = "CK", Database = typeof( SqlDefaultDatabase ), ResourceType = typeof( Package ), ResourcePath = "Res" ), Versions( "1.0.0" )]
    public abstract class Package : SqlPackage
    {
        [InjectContract]
        public ResTable ResTable { get; protected set; }

        [InjectContract]
        public ResStringTable ResStringTable { get; protected set; }

        [InjectContract]
        public ResTextTable ResTextTable { get; protected set; }

        [InjectContract]
        public ResHtmlTable ResHtmlTable { get; protected set; }

        [InjectContract]
        public LCIDTable LCIDTable { get; protected set; }

        [InjectContract]
        public LCIDTable XLCIDTable { get; protected set; }

        [InjectContract]
        public LCIDTable XLCIDMapTable { get; protected set; }

    }
}
