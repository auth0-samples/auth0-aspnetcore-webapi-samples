# Authorization

This example shows how to authorize only users in certain roles to access an API endpoint.

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/03-authorization). 

## Getting Started

To run this quickstart you can fork and clone this repo.

Ensure that you have configured your application in Auth0 to use RS256 for signing JSON Web Tokens, and that you have created the Rule described in the quickstart.

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

### 1. Limit API endpoint to only Admin users

```csharp
// /Controllers/PingController.cs

[Authorize(Roles = "admin")]
public IActionResult Admin()
{
    return View();
}
```