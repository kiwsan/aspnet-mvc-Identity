using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;

namespace IdentityWebApp.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            string clientId = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientId))
                return Task.FromResult<object>(null);

            string refreshTokenId = Guid.NewGuid().ToString("n");

            //set new refresh token

            return Task.FromResult<object>(null);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            string allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            //remove refresh token

            return Task.FromResult<object>(null);
        }
    }
}