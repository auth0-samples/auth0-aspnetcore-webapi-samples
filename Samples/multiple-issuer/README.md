# Authenticate with JWT (RS256)

This example shows how to configure the JWT middleware when handling tokens coming from multiple issuers. 

## Getting Started

To run this sample  you can fork and clone this repo.

Ensure that you have configured your applications and APIs in Auth0 to use RS256 for signing JSON Web Tokens. Also update the middleware registration code to specify your own issuers and audience.

Restore the NuGet packages and run the application:

```bash
# Install the dependencies
dotnet restore

# Run
dotnet run
```

You can shut down the web server manually by pressing Ctrl-C.

## Important Snippets

### 1. Register JWT middleware

```csharp
// /Startup.cs

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    loggerFactory.AddConsole(Configuration.GetSection("Logging"));
    loggerFactory.AddDebug();

    string[] issuers = {
        "https://jerrie.auth0.com/",
        "https://auth0pnp.auth0.com/"
    };

    var keyResolver = new MultipleIssuerSigningKeyResolver();
    var options = new JwtBearerOptions
    {
        TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = "https://rs256.test.api",
            ValidIssuers = new List<string>(issuers),
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) => keyResolver.GetSigningKey(securityToken.Issuer, kid)
        }
    };
    app.UseJwtBearerAuthentication(options);

    app.UseMvc();
}
```