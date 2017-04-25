using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebAPIApplication
{
    public class HasScopeRequirement : AuthorizationHandler<HasScopeRequirement>, IAuthorizationRequirement
    {
        private readonly string issuer;
        private readonly string scope;

        public HasScopeRequirement(string scope, string issuer)
        {
            this.scope = scope;
            this.issuer = issuer;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == issuer).Value.Split(' ');

            // Succeed if the scope array contains the required scope
            if (scopes.Any(s => s == scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}