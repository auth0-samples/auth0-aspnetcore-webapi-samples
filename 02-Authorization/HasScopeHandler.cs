using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebAPIApplication
{
    /// <summary>
    /// An AuthorizationHandler that checks that the current user has
    /// the required scope in its claims.
    /// </summary>
    public class HasScopeHandler: AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            var claim = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer);

            // If user does not have the scope claim, get out of here
            if (claim == null)
                return Task.CompletedTask;

            // Split the scopes string into an array
            var scopes = claim.Value.Split(' ');

            // Succeed if the scope array contains the required scope
            if (scopes.Any(s => s == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
