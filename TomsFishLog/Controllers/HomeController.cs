using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TomsFishLog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return View();                               //TODO put this back after testing. 
            return RedirectToAction("EnterFish", "Fish");  //Redirecting to EnterFish to speed things up while testing.
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}