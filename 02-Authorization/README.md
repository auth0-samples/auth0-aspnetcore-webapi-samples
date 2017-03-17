# Authenticate with JWT (RS256)

This example shows how to use [Custom Policy-Based Authorization](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies) to
require specific scopes in your API.

It includes a `HasScopeRequirement` that lets you create custom policies that require
a specific scope, and a `HasScopeHandler` that will check that the required scope is
present in the principal claims.

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/02-authorization). 

## Getting Started

To run this quickstart you can fork and clone this repo.

Ensure that you have configured your application in Auth0 to use RS256 for signing JSON Web Tokens.

Next, update the `appsettings.json` with your Auth0 settings:

```json
{
  "Auth0": {
    "Domain": "{DOMAIN}",
    "ApiIdentifier": "{API_IDENTIFIER}"
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

### 1. Create custom policies with specific requirements

```csharp
// /Startup.cs ConfigureServices

string domain = $"https://{Configuration["Auth0:Domain"]}/";
services.AddAuthorization(options =>
{
    // these are simple policies, but you can also
    // combine different requirements to create policies
    options.AddPolicy("read:timesheets",
        policy => policy.Requirements.Add(new HasScopeRequirement("read:timesheets", domain)));
    options.AddPolicy("create:timesheets",
        policy => policy.Requirements.Add(new HasScopeRequirement("create:timesheets", domain)));
});
```

### 2. Register the handler for the requirement

```csharp
// /Startup.cs ConfigureServices

// register the scope authorization handler
services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
```

### 3. Add policy requirements in controller actions

```csharp
[Route("api/timesheets")]
public class TimesheetsController : Controller
{
    [Authorize(Policy="read:timesheets")]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Json(new Timesheet[] 
        {
            new Timesheet
            {
                Date = DateTime.Now,
                Employee = "Peter Parker",
                Hours = 8.5F
            },
            new Timesheet
            {
                Date = DateTime.Now.AddDays(-1),
                Employee = "Peter Parker",
                Hours = 7.5F
            }
        });
    }

    [Authorize(Policy="create:timesheets")]
    [HttpPost]
    public IActionResult Create(Timesheet timeheet)
    {
        return Created("http://localhost:5000/api/timeheets/1", timeheet);
    }
}
```