using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        public ActionResult BlogIndex()
        {
            ViewBag.Page = "BlogIndex";
            return View( "BlogIndex." + RouteData.Values["culture"] );
        }

        public ActionResult Admin()
        {
            ViewBag.Page = "Admin";
            return View();
        }

    }
}
