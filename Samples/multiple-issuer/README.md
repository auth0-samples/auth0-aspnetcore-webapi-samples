# Authenticate with JWT (RS256)

This example shows how to configure the JWT middleware when handling tokens coming from multiple issuers. 

## To run this project

### Using the command line

Restore the NuGet packages and run the application:

```bash
dotnet restore

dotnet run
```

### Using Docker

In order to run the example with docker you need to have **Docker** installed.

Execute in command line `sh exec.sh` to run the Docker in Linux or macOS, or `.\exec.ps1` to run the Docker in Windows.

## Calling the API

Go to `http://localhost:3010/api/public` in Postman (or your web browser) to access the ping API endpoint. To access the secure endpoint you will need to [obtain an access token](https://auth0.com/docs/tokens/access-token#how-to-get-an-access-token) and then pass the access token as a **Bearer** token in the **Authorization** header when calling the `http://localhost:3010/api/private` endpoint.

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
            ValidAudience = "https://quickstarts/api",
            ValidIssuers = new List<string>(issuers),
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) => keyResolver.GetSigningKey(securityToken.Issuer, kid)
        }
    };
    app.UseJwtBearerAuthentication(options);

    app.UseMvc();
}
```
