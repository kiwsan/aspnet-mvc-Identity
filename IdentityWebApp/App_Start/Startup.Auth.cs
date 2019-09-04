using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace IdentityWebApp
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            //MVC application
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieName = "IdentityWebApp",
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/Login")
            });

            //Plugin the OAuth bearer JSON Web Token tokens generation and Consumption will be here
            
        }
    }
}
