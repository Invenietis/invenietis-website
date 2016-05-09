using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Data.Entities;
using Microsoft.AspNet.Mvc;
using Q = Invenietis.Repositories.Queries;
using C = Invenietis.Repositories.Commands;
using Invenietis.Common;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Invenietis.Back.Controllers
{
    public class ClientsController : Controller
    {
        Q.ClientRepository _qRepo;
        C.ClientRepository _cRepo;

        public ClientsController( Q.ClientRepository qRepo, C.ClientRepository cRepo )
        {
            _qRepo = qRepo;
            _cRepo = cRepo;
        }

        [HttpGet]
        public JsonResult GetAll( Q.Filters.OrderFilter oFilter, PaginationInfo pInfo )
        {
            return new JsonResult( _qRepo.GetClients( oFilter, pInfo ) );
        }

        [HttpGet]
        public JsonResult Edit( int id )
        {
            var model = _qRepo.GetClientById( id );

            if( model == null )
            {
                id = _cRepo.CreateClient();
                model = _qRepo.GetClientById( id );
            }

            return new JsonResult( model );
        }

        [HttpPost]
        public JsonResult Save( [FromBody] Client client )
        {
            return new JsonResult( _cRepo.UpdateClient( client ) );
        }

        [HttpDelete]
        public JsonResult Delete( int id )
        {
            return new JsonResult( _cRepo.DeleteClient( id ) );
        }
    }
}
