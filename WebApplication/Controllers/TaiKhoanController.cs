using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TaiKhoanController : Controller
    {
        [AllowAnonymous]
        // GET: TaiKhoan
        public ActionResult Index()
        {
            var taiKhoanVm = new TaiKhoanVm();
            return View(taiKhoanVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TaiKhoanVm taiKhoanVm)
        {
            if (ModelState.IsValid)
            {
                if (ValidateRequest)
                {
                    if (taiKhoanVm.TenTaiKhoan == taiKhoanVm.MatKhau)
                    {
                        var id = new CustomeIdentity(taiKhoanVm.TenTaiKhoan);
                        var roles = new string[] {"nhanvien"};
                        var pricipal = new CustomePricipal(id,roles);
                        Thread.CurrentPrincipal = pricipal;
                        HttpContext.User = pricipal;
                        var data = new UserData
                        {
                            UserId = 1,
                            Email = "laivietanh1986@gmail.com",
                            Address = "4.34 Chung Cu Nhieu Loc",
                            Roles = roles
                        };
                        var userData = JsonConvert.SerializeObject(data);
                        var ticket = new FormsAuthenticationTicket(1, taiKhoanVm.TenTaiKhoan, DateTime.Now,
                            DateTime.Now.AddMinutes(30), true, userData);
                        var strTicket = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strTicket);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                       
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
            }
            return RedirectToAction("Index", "TaiKhoan");
        }
    }

    public class UserData
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string[] Roles { get; set; }
    }
}