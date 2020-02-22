# Extracting a user's information from a JWT using ASP.NET Core Web API

## To run this project

Update the `appsettings.json` with your Auth0 settings:

```json
{
  "Auth0": {
    "Domain": "Your Auth0 domain",
    "Audience": "Your Auth0 Client Id"
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

To access any of the endpoints you will need to [obtain an access token](https://auth0.com/docs/tokens/access-token#how-to-get-an-access-token) and then pass the access token as a **Bearer** token in the **Authorization** header when calling the endpoints.

## Understanding the code

1. As a general rule, all the claims on the JWT which was passed in will be available in the Claims of the User object:

```csharp
public object Claims()
{
    return User.Claims.Select(c => new
    {
        Type = c.Type,
        Value = c.Value
    });
}
```

2. To get the unique identifer of the user in Auth0 you can get the value of the `NameIdentifier` claim. This is the value of the `sub` claim of the JWT which was passed in. You can use this to store a reference to the user in your own database.

```csharp
public object UserId()
{
    // The user's ID is available in the NameIdentifier claim
    string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

    return new
    {
        UserId = userId
    };
}
```

3. Finally, if you need the full user profile, you can use the Auth0.NET SDK to call the `/userinfo` endpoint. First ensure that you have the Auth0.NET SDK installed:

```text
Install-Package Auth0.AuthenticationApi
```

Then, you need to ensure that you save the actual value of the JWT in a claim. You can do this in the `OnTokenValidated` event when registering the JWT middleware:

```csharp
string domain = $"https://{Configuration["Auth0:Domain"]}/";
services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
	options.Authority = domain;
	options.Audience = Configuration["Auth0:Audience"];

	options.Events = new JwtBearerEvents
	{
		OnTokenValidated = context =>
		{
			// Grab the raw value of the token, and store it as a claim so we can retrieve it again later in the request pipeline
			// Have a look at the ValuesController.UserInformation() method to see how to retrieve it and use it to retrieve the
			// user's information from the /userinfo endpoint
			if (context.SecurityToken is JwtSecurityToken token)
			{
				if (context.Principal.Identity is ClaimsIdentity identity)
				{
					identity.AddClaim(new Claim("access_token", token.RawData));
				}
			}

			return Task.CompletedTask;
		}
	};
});
```

Finally, inside your controller you can retreive the value of the access token from the claim, and then use that to call the `GetUserInfoAsync` method of the `AuthenticationApiClient`:

```csharp
public async Task<object> UserInformation()
{
    // Retrieve the access_token claim which we saved in the OnTokenValidated event
    string accessToken = User.Claims.FirstOrDefault(c => c.Type == "access_token").Value;

    // If we have an access_token, then retrieve the user's information
    if (!string.IsNullOrEmpty(accessToken))
    {
        return await _client.GetUserInfoAsync(accessToken);
    }

    return null;
}
```

> **NB**: Retrieving the full user profile should be done in rare circumstances. You should definitely not make a call to the `/userinfo` endpoint every time a user calls one of your API endpoints, as you will be rate limited by Auth0. It is therefore suggested that you only retrieve the actual User Id as demonstrated in (2) above, and use that as a foreign key in your own database.