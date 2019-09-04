using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityWebApp.Providers
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            await Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            context.Validated(claimsIdentity);

            await Task.FromResult(0);
        }
    }
}