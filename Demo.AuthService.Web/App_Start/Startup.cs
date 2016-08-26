using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Demo.AuthService.Web.MoveMe;
using IdentityServer3.Core.Configuration;
using Owin;

namespace Demo.AuthService.Web
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            /* Here we are using the InMemory Client Store, Scope Store and User Service. 
             * These are meant for development use only. In production you would see a 
             * User Service implemented using a real user store such as MembershipReboot. (which we'll do later)
             * 
             * You don't have to map Identity Server to a route like this, however Scott Brady has found 
             * it can keep the OWIN pipeline simple and clean when using other OWIN middleware, 
             * especially if you are emmbedding Identity Server into another project.
             * 
             * Also DON'T FORGET to add RAMMFAR to your web.config.
             * Otherwise some of the IdentityServer3 embedded assets will not be loaded correctly by IIS.
             * <system.webServer>
             *     <modules runAllManagedModulesForAllRequests="true" />
             * </system.webServer>
             * 
             * 
             * You should see some awesomeness now at these endpoints.
             * 
             * Home page: https://localhost:44310/identity
             * OpenID Connect discovery endpoint: https://localhost:44310/identity/.well-known/openid-configuration
             * jwks endpoint to confirm the certificate: https://localhost:44310/identity/.well-known/jwks
             * 
             */
            app.Map(
                "/identity",
                coreApp =>
                {
                    coreApp.UseIdentityServer(new IdentityServerOptions
                    {
                        SiteName = "Standalone Identity Server",
                        SigningCertificate = Cert.Load(),
                        Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),
                        RequireSsl = true
                    });
                });
        }
    }
}