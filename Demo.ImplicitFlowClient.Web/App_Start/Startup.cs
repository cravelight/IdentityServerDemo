using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Demo.ImplicitFlowClient.Web.Startup))]
namespace Demo.ImplicitFlowClient.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DoStandardMvcAppStartStuff();
            ConfigureWhatWeNeedForTheImplicitFlowClient(app);
        }



        /// <summary>
        /// Configure what we need to demonstrate an auth consumer using the Implicit flow.
        /// Note, if you are putting this in a clean MVC project, you'll need the following nuget packages:
        ///  - install-package Microsoft.Owin.Host.SystemWeb
        ///  - install-package Microsoft.Owin.Security.Cookies
        ///  - install-package System.IdentityModel.Tokens.Jwt
        /// </summary>
        private void ConfigureWhatWeNeedForTheImplicitFlowClient(IAppBuilder app)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "TempCookie",
                AuthenticationMode = AuthenticationMode.Passive
            });
        }



        private void DoStandardMvcAppStartStuff()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }

}