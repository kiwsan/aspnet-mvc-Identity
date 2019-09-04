using IdentityWebApp.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace IdentityWebApp
{
    public partial class Startup
    {

        public const string TokenEndpointPath = "/api/token";

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

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true, //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                TokenEndpointPath = new PathString(TokenEndpointPath),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(50),
                Provider = new OAuthProvider(),
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

    }
}
