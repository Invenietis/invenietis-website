using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.LocalizedRoutes.Mvc;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Invenietis.Web.Controllers
{
    [LocalizedRoutes]
    public class LearningController : Controller
    {
        public LearningController()
        {

        }

        public IActionResult Index()
        {
            return this.LocalizedView();
        }

        public IActionResult GetTraining( int id, string name )
        {
            return this.LocalizedView();
        }
    }
}
