using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Page = "Index";
            return View( "Index." + RouteData.Values["culture"] );
        }
        public ActionResult Contact()
        {
            ViewBag.Page = "Contact";
            //return View();
            return View( "Contact." + RouteData.Values["culture"] );
        }
        public ActionResult Legal()
        {
            ViewBag.Page = "";
            //return View();
            return View( "Legal." + RouteData.Values["culture"] );
        }
        public ActionResult Cuke()
        {
            ViewBag.Page = "";
            return View();
        }
        public ActionResult CKMultiPlan()
        {
            ViewBag.Page = "CKMultiPlan";
            return View( "CKMultiPlan." + RouteData.Values["culture"] );
        }
    }
}
