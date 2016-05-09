using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common.Cultures;
using Invenietis.Data;
using Invenietis.Data.Entities;

namespace Invenietis.Repositories.Commands
{
    public class ClientRepository : BaseRepository
    {
        public ClientRepository( CultureProvider c )
            : base( c )
        {

        }

        public int CreateClient()
        {
            using( var db = DataContext.GetDefault() )
            {
                var client = new Client();
                foreach( var c in CultureProvider.SupportedCultures ) client.Cultures.Add( c.Id, String.Empty );
                                      
                return db.Clients.Insert( client );
            }
        }

        public bool UpdateClient( Client client )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.Clients.Update( client );
            }
        }

        public bool DeleteClient( int clientId )
        {
            using( var db = DataContext.GetDefault() )
            {
                return db.Clients.Delete( clientId );
            }
        }
    }
}
