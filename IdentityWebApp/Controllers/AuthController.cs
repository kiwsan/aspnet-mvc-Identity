using IdentityWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

//https://stackoverflow.com/questions/32161429/combine-the-use-of-authentication-both-for-mvc-pages-and-for-web-api-pages/39797768
//https://benfoster.io/blog/aspnet-identity-stripped-bare-mvc-part-1
namespace IdentityWebApp.Controllers
{
    public class AuthController : Controller
    {

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // Invoke the "token" OWIN service to perform the login (POST /api/token)
                // Use Microsoft.Owin.Testing.TestServer to perform in-memory HTTP POST request
                var testServer = TestServer.Create<Startup>();
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", model.UserName),
                    new KeyValuePair<string, string>("password", model.Password)
                };

                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await testServer.HttpClient.PostAsync(Startup.TokenEndpointPath, requestParamsFormUrlEncoded);

                if (tokenServiceResponse.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                    var jsSerializer = new JavaScriptSerializer();
                    var responseData = jsSerializer.Deserialize<Dictionary<string, string>>(responseString);
                    var authToken = responseData["access_token"];
                    //var username = responseData["username"];

                    Request.Headers.Add("Authorization", "Bearer " + authToken);

                    var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, "kiwsan"),
                        new Claim(ClaimTypes.Email, "kiwsan@mail.com"),
                        new Claim(ClaimTypes.Country, "Thailand") }, DefaultAuthenticationTypes.ApplicationCookie);

                    var ctx = Request.GetOwinContext();

                    var authenticateResult = await ctx.Authentication.AuthenticateAsync(DefaultAuthenticationTypes.ExternalBearer);
                    ctx.Authentication.SignOut(DefaultAuthenticationTypes.ExternalBearer);

                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);

                    return Redirect(GetRedirectUrl(model.ReturnUrl));
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");

            return View("Login", model);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            ctx.Authentication.SignOut(DefaultAuthenticationTypes.ExternalBearer);
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }

    }
}
