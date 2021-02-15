# Authorize with JWT (RS256)

This example shows how to authenticate a user using a JSON Web Token (JWT) which is signed using RS256.

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authorization). 

## Run the Project

Update the `appsettings.json` with your Auth0 settings:

```json
{
  "Auth0": {
    "Domain": "Your Auth0 domain",
    "Audience": "Your Auth0 Client Id"
  } 
}
```

Restore the NuGet packages and run the application:

```bash
dotnet restore

dotnet run
```

Once the app is up and running, verify that the public API endpoint can be reached, either by using `curl` or accessing `http://localhost:3010/api/public` in the browser. You should see the following output:

```json
{
  "message": "Hello from a public endpoint! You don't need to be authenticated to see this."
}
```

## Run this project with Docker

In order to run the example with Docker you need to have [Docker](https://docker.com/products/docker-desktop) installed.

To build the Docker image and run the project inside a container, run the following command in a terminal, depending on your operating system:

```
# Mac
sh exec.sh

# Windows (using Powershell)
.\exec.ps1
```

## Calling the API

To access the secure endpoint you will need to [obtain an access token](https://auth0.com/docs/tokens/access-token#how-to-get-an-access-token) and then pass the access token as a **Bearer** token in the **Authorization** header when calling the `http://localhost:3010/api/private` endpoint.

## Important Snippets

### 1. Register Authentication Services & CORS policy

```csharp
// Startup.cs

public void ConfigureServices(IServiceCollection services)
{
    // Leave any code your app/template already has here and just add these lines:
    services.AddCors(options =>
	{
		options.AddPolicy("AllowSpecificOrigin",
			builder =>
			{
				builder
				.WithOrigins("http://localhost:8080")
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials();
			});
	});
	
    var domain = $"https://{Configuration["Auth0:Domain"]}/";
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.Authority = domain;
			options.Audience = Configuration["Auth0:Audience"];
		});
}
```

### 2. Register Authentication Middleware & Enable CORS

```csharp
// Startup.cs

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Leave any code your app/template already has here and just add the line:
    app.UseCors("AllowSpecificOrigin");
	app.UseAuthentication();
	app.UseAuthorization();
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
