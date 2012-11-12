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
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Page = "Contact";
            return View();
        }
        public ActionResult Legal()
        {
            ViewBag.Page = "";
            return View();
        }
    }
}
