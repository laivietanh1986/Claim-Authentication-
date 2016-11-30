using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            var pricipal = HttpContext.User as CustomePricipal;
            var identity = pricipal.Identity as CustomeIdentity;
            ViewBag.Message = $"Your application description page.{identity.Email},{identity.Address},{identity.Id},{identity.Name}";

            return View();
        }
        [Authorize(Roles = "test")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}