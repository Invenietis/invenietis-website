using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;

namespace CK.DB.Resource
{
    [SqlTable( "tRes", Package = typeof( Package ) ), Versions( "1.0.0" )]
    [SqlObjectItem( "fResNamePrefix, fResNameLeaf, fResNamePrefixes" )]
    [SqlObjectItem( "vRes, vRes_ParentPrefixes, vRes_DirectChildren, vRes_AllChildren" )]
    [SqlObjectItem( "sResCreate, sResAssume, sResCreateAuto, sResDestroy, sResDestroyChildren, sResDestroyWithChildren, sResDestroyByPrefix, sResRename" )]
    public abstract class ResTable : SqlTable
    {
        
    }
}
