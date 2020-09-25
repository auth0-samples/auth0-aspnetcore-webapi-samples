# Authenticate with JWT from Multiple Issuers

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

public void ConfigureServices(IServiceCollection services)
{
	services.AddAuthentication()
		.AddJwtBearer("Auth0DomainOne", options =>
		{
			options.Authority = Configuration["Auth0:DomainOne"];
			options.Audience = Configuration["Auth0:Audience"];

		})
		.AddJwtBearer("Auth0DomainTwo", options =>
		{
			options.Authority = Configuration["Auth0:DomainTwo"];
			options.Audience = Configuration["Auth0:Audience"];
		});

	services
		.AddAuthorization(options =>
		{
			options.DefaultPolicy = new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.AddAuthenticationSchemes("Auth0DomainOne", "Auth0DomainTwo")
				.Build();
		});

	services.AddControllers();
}
```
