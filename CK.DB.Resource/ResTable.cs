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
    public abstract class ResTable : SqlTable
    {
        public const string ResNamePattern = @"^[a-zA-Z0-9]([a-zA-Z0-9\.])*[a-zA-Z0-9]$";
        public static readonly Regex ResName = new Regex( ResNamePattern, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline );

        /// <summary>
        /// Creates a new resource. It must not already exist, otherwise an exception is thrown.
        /// Use <see cref="CmdAssume"/> command to find or create a resource.
        /// </summary>
        /// <param name="ctx">Call context.</param>
        /// <param name="resName">Resource name.</param>
        /// <returns>The new resource identifier.</returns>
        [SqlProcedureNonQuery( "sResCreate" )]
        public abstract int Create( ISqlCallContext ctx, string resName );

        /// <summary>
        /// Creates a new resource. It must not already exist, otherwise an exception is thrown.
        /// Use <see cref="CmdAssume"/> command to find or create a resource.
        /// </summary>
        /// <param name="ctx">Call context.</param>
        /// <param name="resName">Resource name.</param>
        /// <returns>The new resource identifier.</returns>
        [SqlProcedureNonQuery( "sResCreate" )]
        public abstract Task<int> CreateAsync( ISqlCallContext ctx, string resName );

        /// <summary>
        /// Finds or creates a resource.
        /// </summary>
        /// <param name="ctx">Call context.</param>
        /// <param name="resName">Resource name.</param>
        /// <returns>The resource identifier.</returns>
        [SqlProcedureNonQuery( "sResAssume" )]
        public abstract int Assume( ISqlCallContext ctx, string resName );

        /// <summary>
        /// Finds or creates a resource.
        /// </summary>
        /// <param name="ctx">Call context.</param>
        /// <param name="resName">Resource name.</param>
        /// <returns>The resource identifier.</returns>
        [SqlProcedureNonQuery( "sResAssume" )]
        public abstract Task<int> AssumeAsync( ISqlCallContext ctx, string resName );

        /// <summary>
        /// Creates an automatically named resource (with a Guid) under a given prefix (ex: 'R.Auto.168A1D7E-7257-4DC2-AF68-86A809F8ECB2').
        /// </summary>
        /// <param name="ctx">Call context.</param>
        /// <param name="prefix">Optional prefix.</param>
        /// <returns>The new resource identifier.</returns>
        [SqlProcedureNonQuery( "sResCreateAuto" )]
        public abstract int CreateAuto( ISqlCallContext ctx, string prefix = "R.Auto" );

        /// <summary>
        /// Creates an automatically named resource (with a Guid) under a given prefix (ex: 'R.Auto.168A1D7E-7257-4DC2-AF68-86A809F8ECB2').
        /// </summary>
        /// <param name="ctx">Call context.</param>
        /// <param name="prefix">Optional prefix.</param>
        /// <returns>The new resource identifier.</returns>
        [SqlProcedureNonQuery( "sResCreateAuto" )]
        public abstract Task<int> CreateAutoAsync( ISqlCallContext ctx, string prefix = "R.Auto" );

        /// <summary>
        /// Suppres the given resource.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="resId"></param>
        [SqlProcedureNonQuery( "sResDestroy" )]
        public abstract void Destroy( ISqlCallContext ctx, int resId );

        [SqlProcedureNonQuery( "sResDestroyChildren" )]
        public abstract void DestroyChildren( ISqlCallContext ctx, int resId );

        [SqlProcedureNonQuery( "sResDestroyWithChildren" )]
        public abstract void DestroyWithChildren( ISqlCallContext ctx, int resId );

        [SqlProcedureNonQuery( "sResDestroyByPrefix" )]
        public abstract void DestroyByPrefix( ISqlCallContext ctx, string resNamePrefix );

        [SqlProcedureNonQuery( "sResRename" )]
        public abstract void Rename( ISqlCallContext ctx, int resId, string newName, bool withChildren = true );

    }
}
