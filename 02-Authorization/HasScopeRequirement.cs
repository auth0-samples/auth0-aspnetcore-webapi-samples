using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace WebAPIApplication
{
    /// <summary>
    /// Represents a requirement of a scope from a specific issuer.
    /// </summary>
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Gets the issuer for the required scope.
        /// </summary>
        public string Issuer { get; private set; }

        /// <summary>
        /// Gets the required scope.
        /// </summary>
        public string Scope { get; private set; }

        /// <summary>
        /// Creates a new requirement, using the specified scope and issuer.
        /// </summary>
        /// <param name="scope">The required scope.</param>
        /// <param name="issuer">The issuer of the scope.</param>
        public HasScopeRequirement(string scope, string issuer)
        {
            if (string.IsNullOrEmpty(scope))
                throw new ArgumentNullException(nameof(scope));

            if (string.IsNullOrEmpty(issuer))
                throw new ArgumentNullException(nameof(issuer));

            this.Scope = scope;
            this.Issuer = issuer;
        }
    }
}