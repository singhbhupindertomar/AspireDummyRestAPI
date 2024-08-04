using AspireSmallFinance.Models.Entities;
using AspireSmallFinance.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Web.Http;

namespace AspireSmallFinance.Handler
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthenticationServices _authenticationServices;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IAuthenticationServices authenticationServices) 
            : base(options, logger, encoder, clock)
        {
            _authenticationServices = authenticationServices;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return AuthenticateResult.NoResult();
            }

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                Context.Response.StatusCode = 401;
                return AuthenticateResult.Fail("Unable to process/parse authorization header.");
            }

            Users? user = null;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                user = await _authenticationServices.Login(username,password);
            }
            catch
            {
                Context.Response.StatusCode = 401;
                return AuthenticateResult.Fail("Unable to process/parse authorization header.");
            }

            if (user == null)
                return AuthenticateResult.Fail("Unable to authenticate the user.");

            var claims = new[] {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.PrimarySid, user.UserSysId.ToString()),
                new Claim(ClaimTypes.Role, user.IsAdminFlag ? "Admin" : "User")
            };


            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
