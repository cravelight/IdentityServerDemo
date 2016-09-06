using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Demo.AuthService.Web.MoveMe;
using Demo.AuthService.Web.MoveMe.Clients;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Serilog;

[assembly: OwinStartup(typeof(Demo.AuthService.Web.Startup))]
namespace Demo.AuthService.Web
{

    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DoTypicalMvcAndWebApiApplicationStart();

            // This sets up the resource server side of things
            // the part where someone tries to connect with a 
            // protected resource and has to authenticate first

            // todo: create a setup method that spins up step 2 of scott brady's article
            // we'll go all the way through and create a special controller to see it work
            // I need to figure out what "flow" that's called and use the term in my comments
            //
            // then we get to what we really want - the hybrid flow, which is what I think will
            // be the dominant one used. we need to set that up as well and document what it is
            //
            // finally I think there is an api only flow or something - might need to look at that

            // side note - the name of this method is wrong - we should say what flow we are
            // setting up for.
            //var redirectUri = applicationBaseUri;
            //HybridFlowClientSupport(app, identityServerUri, redirectUri);




            // This sets up the Auth Server and Token Service side of things.
            BootstrapIdentityServer(app, DemoConstants.IdentityServerIdentityEndpoint);
        }


        /// <summary>
        /// Since we need OWIN for IdentityServer anyway we might as well use OWIN Startup 
        /// to set up our normal Application_Start stuff. This gets rid of our need for a 
        /// global.asax. The only downside is that without a global.asax we don't have access
        /// to standard application events like session start/end. However, I don't think we 
        /// care since we'd really like to not have to use sessions anyway.
        /// </summary>
        private void DoTypicalMvcAndWebApiApplicationStart()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        /// <summary>
        /// This part sets up the authorization and token service.
        /// It requires the following nuget packages:
        /// - install-package IdentityServer3
        /// - install-package Microsoft.Owin.Host.Systemweb
        /// ------------------------------------------------
        /// 
        /// Here we are using the InMemory Client Store, Scope Store and User Service. 
        /// These are meant for development use only. In production you would see a 
        /// User Service implemented using a real user store such as MembershipReboot. (which we'll do later)
        /// 
        /// You don't have to map Identity Server to a route like this, however Scott Brady has found 
        /// it can keep the OWIN pipeline simple and clean when using other OWIN middleware, 
        /// especially if you are emmbedding Identity Server into another project.
        /// 
        /// Also DON'T FORGET to add RAMMFAR to your web.config.
        /// Otherwise some of the IdentityServer3 embedded assets will not be loaded correctly by IIS.
        /// <system.webServer>
        ///     <modules runAllManagedModulesForAllRequests="true" />
        /// </system.webServer>
        /// 
        /// 
        /// You should see some awesomeness now at these endpoints.
        /// 
        /// Home page: https://localhost:44310/identity
        /// OpenID Connect discovery endpoint: https://localhost:44310/identity/.well-known/openid-configuration
        /// jwks endpoint to confirm the certificate: https://localhost:44310/identity/.well-known/jwks
        /// 
        /// </summary>
        private void BootstrapIdentityServer(IAppBuilder app, string identityServerEndpoint)
        {
            //https://identityserver.github.io/Documentation/docsv2/configuration/logging.html
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .LiterateConsole(outputTemplate: "{Timestamp:HH:MM} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
                .CreateLogger();

            app.Map(
                identityServerEndpoint,
                coreApp =>
                {
                    coreApp.UseIdentityServer(new IdentityServerOptions
                    {
                        SiteName = "Authentication Service Demo",
                        SigningCertificate = DemoConstants.GetX509Certificate2(),
                        Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(ClientCollection.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),
                        RequireSsl = true
                    });
                });
        }


        /// <summary>
        /// HybridFlow
        /// This part sets up the support we need for the MVC client pages.
        /// It requires the following nuget packages:
        /// - install-package Microsoft.Owin.Security.Cookies
        /// - install-package Microsoft.Owin.Security.OpenIdConnect
        /// </summary>
        //private void HybridFlowClientSupport(IAppBuilder app, string authorityUri, string redirectUri)
        //{
        //    // Configure the cookie middleware with its default values
        //    app.UseCookieAuthentication(new CookieAuthenticationOptions
        //    {
        //        AuthenticationType = "Cookies"
        //    });

        //    // Point the OpenID Connect middleware(also in Startup.cs) to our embedded version of IdentityServer
        //    app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
        //    {
        //        Authority = authorityUri,
        //        ClientId = "mvc",
        //        RedirectUri = redirectUri,
        //        ResponseType = "id_token",

        //        SignInAsAuthenticationType = "Cookies"
        //    });
        //}



    }
}