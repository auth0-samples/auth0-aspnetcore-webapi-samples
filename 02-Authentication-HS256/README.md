# Authenticate with JWT (HS256)

This example shows how to authenticate a user using a JSON Web Token (JWT) which is signed using HS256.

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/02-authentication-hs256). 

## Getting Started

To run this quickstart you can fork and clone this repo.

Ensure that you have configured your application in Auth0 to use HS256 for signing JSON Web Tokens.

Next, update the `appsettings.json` with your Auth0 settings:

```json
{
  "Auth0": {
    "Domain": "Your Auth0 domain",
    "ClientId": "Your Auth0 Client Id",
    "ClientSecret": "Your Auth0 Client Secret"
  } 
}
```

Then restore the NuGet packages and run the application:

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

var keyAsBase64 = Configuration["auth0:clientSecret"].Replace('_', '/').Replace('-', '+');
var keyAsBytes = Convert.FromBase64String(keyAsBase64);

var options = new JwtBearerOptions
{
    TokenValidationParameters =
    {
        ValidIssuer = $"https://{Configuration["auth0:domain"]}/",
        ValidAudience = Configuration["auth0:clientId"],
        IssuerSigningKey = new SymmetricSecurityKey(keyAsBytes)                
    }
};
app.UseJwtBearerAuthentication(options);
```

### 2. Secure an API method

```csharp
// /Controllers/PingController.cs

[Route("api")]
public class PingController : Controller
{
    [Authorize]
    [HttpGet]
    [Route("ping/secure")]
    public string PingSecured()
    {
        return "All good. You only get this message if you are authenticated.";
    }
}
```
