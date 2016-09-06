using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Demo.ImplicitFlowClient.Web.Controllers
{
    public class AccountController : Controller
    {


        #region Send user to the auth server

        public ActionResult SignIn()
        {
            var state = Guid.NewGuid().ToString("N");
            var nonce = Guid.NewGuid().ToString("N");

            var url = DemoConstants.IdentityServerAuthorizationUri +
                "?client_id=" + DemoConstants.ImplicitClientId +
                "&response_type=id_token" +
                "&scope=openid email profile" +
                "&redirect_uri=" + DemoConstants.ImplicitClientRedirectUri +
                "&response_mode=form_post" +
                "&state=" + state +
                "&nonce=" + nonce;

            this.SetTempCookie(state, nonce);
            return this.Redirect(url);
        }

        private void SetTempCookie(string state, string nonce)
        {
            var tempId = new ClaimsIdentity("TempCookie");
            tempId.AddClaim(new Claim("state", state));
            tempId.AddClaim(new Claim("nonce", nonce));

            this.Request.GetOwinContext().Authentication.SignIn(tempId);
        }

        #endregion // Send user to the auth server



        #region Receive postback from the auth server after the user validates their credentials

        [HttpPost]
        public async Task<ActionResult> SignInCallback()
        {
            var token = this.Request.Form["id_token"];
            var state = this.Request.Form["state"];

            var claims = await ValidateIdentityTokenAsync(token, state);
            var id = new ClaimsIdentity(claims, "Cookies");
            this.Request.GetOwinContext().Authentication.SignIn(id);

            return this.Redirect("/");
        }

        private async Task<IEnumerable<Claim>> ValidateIdentityTokenAsync(string token, string state)
        {
            var cert = DemoConstants.GetX509Certificate2();

            var result = await this.Request
                .GetOwinContext()
                .Authentication
                .AuthenticateAsync("TempCookie");

            if (result == null)
            {
                throw new InvalidOperationException("No temp cookie");
            }

            if (state != result.Identity.FindFirst("state").Value)
            {
                throw new InvalidOperationException("invalid state");
            }

            var parameters = new TokenValidationParameters
            {
                ValidAudience = DemoConstants.ImplicitClientId,
                ValidIssuer = DemoConstants.IdentityServerUri,
                IssuerSigningKey = new X509SecurityKey(cert),
                // from Scott Brady's 2nd post, but I think the library updated to the above 
                // IssuerSigningToken = new X509SecurityToken(cert)
            };

            var handler = new JwtSecurityTokenHandler();
            SecurityToken jwt;
            var id = handler.ValidateToken(token, parameters, out jwt);

            if (id.FindFirst("nonce").Value != result.Identity.FindFirst("nonce").Value)
            {
                throw new InvalidOperationException("Invalid nonce");
            }

            this.Request.GetOwinContext().Authentication.SignOut("TempCookie");

            return id.Claims;
        }

        #endregion // Receive postback from the auth server after the user validates their credentials



        #region Signout

        public ActionResult SignOut()
        {
            this.Request.GetOwinContext().Authentication.SignOut();
            return this.Redirect(DemoConstants.IdentityServerLogoutUri);
        }

        #endregion // Signout





    }
}