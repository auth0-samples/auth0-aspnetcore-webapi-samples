# Authenticate with JWT (HS256)

This example shows how to authenticate a user using a JSON Web Token (JWT) which is signed using HS256.

## To run this project

Update the `appsettings.json` with your Auth0 settings:

```json
{
  "Auth0": {
    "Domain": "Your Auth0 domain",
    "Audience": "Your Auth0 Client Id",
    "ApiSecret": "Your API secret"
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
    string domain = $"https://{Configuration["Auth0:Domain"]}/";
	string audience = Configuration["Auth0:Audience"];
	string apiSecret = Configuration["Auth0:ApiSecret"];
	services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{ 
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidIssuer = domain,
				ValidAudience = audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSecret))
			};
		});
	
	services.AddAuthorization(options =>
	{
		options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
	});

	services.AddControllers();
}
```

### 2. Register Authentication Middleware

```csharp
// Startup.cs

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
	// ...

	app.UseAuthentication();
	app.UseAuthorization();

	// ...
}
```

### 3. Secure an API method

```csharp
// /Controllers/ApiController.cs

[Route("api")]
public class ApiController : Controller
{
    [HttpGet]
    [Route("private")]
    [Authorize]
    public IActionResult Private()
    {
        return Json(new
        {
            Message = "Hello from a private endpoint! You need to be authenticated to see this."
        });
    }
}
```
