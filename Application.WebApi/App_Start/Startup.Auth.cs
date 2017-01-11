using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Application.Data;
using Microsoft.AspNet.Identity;
using Application.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(Application.WebApi.App_Start.Startup))]

namespace Application.WebApi.App_Start
{
    public partial class Startup
    {
        public void ConfigurationAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<UserManager<ApplicationUser>>(CreateManager);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/oauth/token"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
                //RefreshTokenProvider = new ApplicationRefreshTokenProvider(),
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }


        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();

            }
            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

                if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                UserManager<ApplicationUser> userManager = context.OwinContext.GetUserManager<UserManager<ApplicationUser>>();
                ApplicationUser user;
                try
                {
                    user = await userManager.FindAsync(context.UserName, context.Password);
                }
                catch
                {
                    // Could not retrieve the user due to error.
                    context.SetError("server_error");
                    context.Rejected();
                    return;
                }
                if (user != null)
                {
                    //var listRoles = await userManager.GetRolesAsync(user.Id);
                    //if (!(listRoles.Contains("Admin") || listRoles.Contains("full_control")))
                    //{
                    //    context.SetError("invalid_grant", "Bạn không có quyền!");
                    //    context.Rejected();
                    //}
                    //else
                    {
                        ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                                   user,
                                   DefaultAuthenticationTypes.ExternalBearer);
                        context.Validated(identity);
                    }

                }
                else
                {
                    context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng.'");
                    context.Rejected();
                }
            }
            public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
            {
                // Change authentication ticket for refresh token requests  
                var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
                newIdentity.AddClaim(new Claim("newClaim", "newValue"));

                var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
                context.Validated(newTicket);

                return Task.FromResult<object>(null);
            }
        }

        private static UserManager<ApplicationUser> CreateManager(IdentityFactoryOptions<UserManager<ApplicationUser>> options, IOwinContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>());
            var owinManager = new UserManager<ApplicationUser>(userStore);
            return owinManager;
        }
    }
}
