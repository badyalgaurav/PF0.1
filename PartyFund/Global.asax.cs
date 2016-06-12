using Newtonsoft.Json;
using PartyFund.Presentation.UI.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace PartyFund
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {

                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.UserDetailsID = serializeModel.UserDetailsID;
                newUser.UserName = serializeModel.UserName;
                newUser.Email = serializeModel.Email;
                newUser.RoleId = serializeModel.RoleId;
                newUser.ID = serializeModel.ID;

                HttpContext.Current.User = newUser;
            }
        }

        //private void Application_Error(object sender, EventArgs e)
        //{
        //    Exception ex = Server.GetLastError();

        //    if (ex is HttpAntiForgeryException)
        //    {
        //        Response.Clear();
        //        Server.ClearError(); //make sure you log the exception first
        //        Response.Redirect("~/Account/Login", true);
        //    }
        //}
    }
}