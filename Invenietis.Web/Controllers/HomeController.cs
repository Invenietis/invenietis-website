using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.LocalizedRoutes.Mvc;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Invenietis.Web.Controllers
{

    [LocalizedRoutes]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.LocalizedView();
        }

        public IActionResult BusinessSystems()
        {
            return this.LocalizedView();
        }

        public IActionResult AboutUs()
        {
            return this.LocalizedView();
        }

        public IActionResult Consulting()
        {
            return this.LocalizedView();
        }

        public IActionResult ResearchAndDev()
        {
            return this.LocalizedView();
        }

        public IActionResult Error()
        {
            return this.LocalizedView();
        }
    }
}
