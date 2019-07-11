# Authorize with JWT (RS256)

This example shows how to authenticate a user using a JSON Web Token (JWT) which is signed using RS256.

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authorization). 

## To run this project

Update the `appsettings.json` with your Auth0 settings:

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

### Using Docker

In order to run the example with docker you need to have **Docker** installed.

Execute in command line `sh exec.sh` to run the Docker in Linux or macOS, or `.\exec.ps1` to run the Docker in Windows.

## Calling the API

Go to `http://localhost:3010/api/public` in Postman (or your web browser) to access the ping API endpoint. To access the secure endpoint you will need to [obtain an access token](https://auth0.com/docs/tokens/access-token#how-to-get-an-access-token) and then pass the access token as a **Bearer** token in the **Authorization** header when calling the `http://localhost:3010/api/private` endpoint.

## Important Snippets

### 1. Register Authentication Services

```csharp
// Startup.cs

public void ConfigureServices(IServiceCollection services)
{
    // Leave any code your app/template already has here and just add the lines:
    
	var domain = $"https://{Configuration["Auth0:Domain"]}/";
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = Configuration["Auth0:ApiIdentifier"];
    });
}
```

### 2. Register Authentication Middleware

```csharp
// Startup.cs

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    // Leave any code your app/template already has here and just add the line:
    app.UseAuthentication();
}
```

### 3. Secure an API method

```csharp
// /Controllers/ApiController.cs

[Route("api/[controller]")]
[ApiController]
public class YourController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult Private()
    {
        return Ok(new
        {
            Message = "Hello from a private endpoint! You need to be authenticated to see this."
        });
    }
}
```
