using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Data;
using Invenietis.Data.Entities;

namespace Invenietis.Repositories.Queries
{
    public class ClientRepository
    {
        public ClientRepository()
        {

        }

        public Client GetClientById( int clientId )
        {
            using( var db = new DataContext() )
            {
                var client = db.Clients.FindById( clientId );

                return client;
            }
        }

        public IEnumerable<Client> GetClients()
        {
            using( var db = new DataContext() )
            {
                return db.Clients.FindAll().ToList();
            }
        }
    }
}
