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

        public ActionResult Index()
        {
            ViewBag.Page = "BlogIndex";
            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Page = "Admin";
            return View();
        }

    }
}
