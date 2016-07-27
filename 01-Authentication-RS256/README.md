# Authenticate with JWT (RS256)

This example shows how to authenticate a user using a JSON Web Token (JWT) which is signed using RS256.

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authentication-rs256). 

## Getting Started

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

var options = new JwtBearerOptions
{
    Audience = Configuration["auth0:clientId"],
    Authority = $"https://{Configuration["auth0:domain"]}/"
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