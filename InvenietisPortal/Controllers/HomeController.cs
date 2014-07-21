using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CK.Core;
using CK.Mailer;
using InvPortal.Models;

namespace InvPortal.Controllers
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

        [HttpPost]
        public ActionResult Support( SupportEmailViewModel model )
        {
            IActivityMonitor m = new ActivityMonitor();
            if( ModelState.IsValid )
            {
                using (m.OpenInfo().Send( "Sending Support mail to {0}", ConfigurationManager.AppSettings.Get( "DestinationEmail" ) ))
                {
                    try
                    {
                        IMailerService mailer = new DefaultMailerService();
                        mailer.SendMail( model, new RazorMailTemplateKey( "SupportEmail" ), new Recipient( ConfigurationManager.AppSettings.Get( "DestinationEmail" ) ) );
                    }
                    catch (Exception ex)
                    {
                        m.Error().Send( ex, "Email : {0}, Subject : {1}, Body : {2}", model.Email, model.Subject, model.Body );
                        return PartialView( "_EmailNotSent", model );
                    }
                }

                return PartialView( "_EmailSent" );
            }
            return PartialView( "_Form", model );
        }
    }
}
