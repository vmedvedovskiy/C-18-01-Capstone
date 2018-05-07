using System;
using System.Security.Claims;
using System.Threading.Tasks;
using C_18_01_Capstone.Services;
using C_18_01_Capstone.Services.Services;
using Microsoft.Owin.Security.OAuth;

namespace C_18_01_Capstone.API.Infrastructure
{
    public class AuthorizationServerProvider 
        : OAuthAuthorizationServerProvider, IOAuthAuthorizationServerProvider
    {
        private IUserService userService;

        public AuthorizationServerProvider(IUserService userService)
        {
            this.userService = userService;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string login = context.Parameters["Login"];
            string hashedPassword = context.Parameters["HashedPassword"];
            UserModel user = userService.FindUser(login);

            if (hashedPassword.Equals(user.HashedPassword,
                StringComparison.Ordinal))
            {
                context.Validated();
            }

            return Task.CompletedTask;
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.Validated(
                new ClaimsIdentity(context.Options.AuthenticationType));

            return Task.CompletedTask;
        }
    }
}