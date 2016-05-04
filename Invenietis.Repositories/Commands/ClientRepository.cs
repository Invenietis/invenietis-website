using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Data;
using Invenietis.Data.Entities;

namespace Invenietis.Repositories.Commands
{
    public class ClientRepository
    {
        public ClientRepository()
        {

        }

        public int CreateClient( Client client )
        {
            using( var db = new DataContext() )
            {
                return db.Clients.Insert( client );
            }
        }

        public bool UpdateClient( Client client )
        {
            using( var db = new DataContext() )
            {
                return db.Clients.Update( client );
            }
        }

        public bool DeleteClient( int clientId )
        {
            using( var db = new DataContext() )
            {
                return db.Clients.Delete( clientId );
            }
        }
    }
}
