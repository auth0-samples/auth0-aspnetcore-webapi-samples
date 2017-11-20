# Authorize with JWT (RS256)

This example shows how to authenticate a user using a JSON Web Token (JWT) which is signed using RS256.

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authorization). 

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
