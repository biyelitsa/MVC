using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
        public ActionResult Contact2()
        {
            // 1.YOL-A ViewBag
            Webmaster wm = new Webmaster();
            wm.Ad = "Şamil";
            wm.Mail = "samilyilmaz@gmail.com";
            wm.Tel = 145236521;

            ViewBag.Ad = wm.Ad;
            ViewBag.Mail = wm.Mail;
            ViewBag.Tel = wm.Tel;

            // 1.YOL-B ViewBag
            ViewBag.Webmaster = wm;

            return View();
        }
        public ActionResult Contact3()
        {
            // 2.YOL MODEL
            Webmaster wm = new Webmaster();
            wm.Ad = "Şamil";
            wm.Mail = "samilyilmaz@gmail.com";
            wm.Tel = 145236521;

            return View(wm);
        }
    }
}