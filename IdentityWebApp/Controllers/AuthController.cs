using IdentityWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

//https://github.com/deepumi/AspNetMVC5Authorization
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
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, "kiwsan"),
                    new Claim(ClaimTypes.Email, "kiwsan@mail.com"),
                    new Claim(ClaimTypes.Country, "Thailand")
                }, DefaultAuthenticationTypes.ApplicationCookie);

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            ModelState.AddModelError("", "Invalid login attempt.");

            return View(model);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

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
