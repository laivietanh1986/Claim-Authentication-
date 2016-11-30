using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    UserData userData = JsonConvert.DeserializeObject<UserData>(ticket.UserData);
                    var id = new CustomeIdentity(ticket.Name)
                    {
                        Email = userData.Email,
                        Address = userData.Address,
                        Id = userData.UserId
                    };
                    var roles = Roles.GetRolesForUser(ticket.Name);
                    var principal = new CustomePricipal(id,roles);
                    //var principal = new CustomePricipal(id,userData.Roles);
                    HttpContext.Current.User = principal;
                }
            }
            
        }
    }
}
