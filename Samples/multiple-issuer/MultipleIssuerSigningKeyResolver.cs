using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace WebAPIApplication
{
    public class MultipleIssuerSigningKeyResolver
    {
        private readonly IDictionary<string, ConfigurationManager<OpenIdConnectConfiguration>> _configurationManagers =
            new Dictionary<string, ConfigurationManager<OpenIdConnectConfiguration>>();

        public IEnumerable<SecurityKey> GetSigningKey(string issuer, string kid)
        {
            // See if we have a Configuration Manager for this issuer
            ConfigurationManager<OpenIdConnectConfiguration> configurationManager = null;

            // If not, create one
            if (!_configurationManagers.TryGetValue(issuer, out configurationManager))
            {
                configurationManager =
                    new ConfigurationManager<OpenIdConnectConfiguration>(
                        $"{issuer.TrimEnd('/')}/.well-known/openid-configuration",
                        new OpenIdConnectConfigurationRetriever());
                _configurationManagers[issuer] = configurationManager;
            }

            // Get the OIDC config
            var openIdConfig = AsyncHelper.RunSync(async () => await configurationManager.GetConfigurationAsync());

            // return the signing key(s) which matches the requested kid
            return openIdConfig.SigningKeys.Where(sk => sk.KeyId == kid);
        }
    }
}