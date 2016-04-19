using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.LocalizedRoutes
{
    public class LocalizedRouteEventArgs : EventArgs
    {
        public LocalizedRouteEventArgs( ILocalizedRoute localizedRoute )
        {
            if( localizedRoute == null ) throw new ArgumentNullException( nameof( localizedRoute ) );
            LocalizedRoute = localizedRoute;
        }

        public ILocalizedRoute LocalizedRoute { get; private set; }
    }
}
