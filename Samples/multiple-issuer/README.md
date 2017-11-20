# Authenticate with JWT (RS256)

This example shows how to configure the JWT middleware when handling tokens coming from multiple issuers. 

## To run this project

To run this quickstart you can fork and clone this repo.

Ensure that you have configured your application in Auth0 to use RS256 for signing JSON Web Tokens.

Next, update the `appsettings.json` with your Auth0 settings:

```json
{
  "Auth0": {
    "Domain": "Your Auth0 domain",
    "ClientId": "Your Auth0 Client Id"
  } 
}
```

### Using the command line

Restore the NuGet packages and run the application:

```bash
dotnet restore

dotnet run
```

Then go to `http://localhost:5000/api/ping` in Postman (or your web browser) to access the ping API endpoint.

To access the secure endpoint you will need to [obtain an access token](https://auth0.com/docs/tokens/access-token#how-to-get-an-access-token). and then pass the access token as a **Bearer** token in the **Authorization** header when calling the `http://localhost:5000/api/ping/secure` endpoint.

### Using Docker

In order to run the example with docker you need to have `docker` installed.

Execute in command line `sh exec.sh` to run the Docker in Linux, or `.\exec.ps1` to run the Docker in Windows.

Then go to `http://localhost:3010/api/ping` in Postman (or your web browser) to access the ping API endpoint.

To access the secure endpoint you will need to [obtain an access token](https://auth0.com/docs/tokens/access-token#how-to-get-an-access-token). and then pass the access token as a **Bearer** token in the **Authorization** header when calling the `http://localhost:3010/api/ping/secure` endpoint.


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
