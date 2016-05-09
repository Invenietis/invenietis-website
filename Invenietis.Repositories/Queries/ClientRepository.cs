using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common;
using Invenietis.Data;
using Invenietis.Data.Entities;
using Invenietis.Repositories.Queries.Filters;
using LiteDB;

namespace Invenietis.Repositories.Queries
{
    public class ClientRepository
    {
        public ClientRepository()
        {

        }

        public Client GetClientById( int clientId )
        {
            using( var db = DataContext.GetDefault() )
            {
                var client = db.Clients.FindById( clientId );

                return client;
            }
        }

        public PaginatedResult<Client> GetClients( OrderFilter oFilter, PaginationInfo pInfo )
        {
            using( var db = DataContext.GetDefault() )
            {
                var total = db.Clients.Count();

                var qOrder = oFilter.OrderDesc ? -1 : 1;
                Query q = !String.IsNullOrEmpty(oFilter.OrderBy) ? Query.All( oFilter.OrderBy, qOrder ) : Query.All( qOrder );

                var clients = db.Clients.Find(q, pInfo.Page * pInfo.PerPage, pInfo.PerPage).ToArray();

                return new PaginatedResult<Client>( pInfo, clients, total );
            }
        }
    }
}
