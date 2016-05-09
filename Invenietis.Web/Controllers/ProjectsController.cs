using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.LocalizedRoutes.Mvc;
using Microsoft.AspNet.Mvc;
using Q = Invenietis.Repositories.Queries;
using C = Invenietis.Repositories.Commands;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Invenietis.Web.Controllers
{
    [LocalizedRoutes]
    public class ProjectsController : Controller
    {
        Q.ProjectRepository _qRepo;

        public ProjectsController( Q.ProjectRepository qRepo )
        {
            _qRepo = qRepo;
        }

        public IActionResult Index()
        {
            return this.LocalizedView();
        }

        public IActionResult GetProject( string name )
        {
            return this.CustomLocalizedView( $"Pages/{name}" );
        }
    }
}
