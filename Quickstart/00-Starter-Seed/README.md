# Seed Project for the ASP.NET Core Web API Quickstart

This seed project can be used if you want to follow along with the steps in the [ASP.NET Core Web API Quickstart](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi).

This starter seed is a basic web API application which was created using the Yeoman generator for ASP.NET, and also includes  the dependencies required to use the JWT middleware.

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

Then restore the NuGet packages and run the application:

```bash
dotnet restore

dotnet run
```

Then go to `http://localhost:5000/api/ping` in Postman (or your web browser) to access the ping API endpoint.

## Running th sample with Docker

In order to run the example with docker you need to have `docker` installed.

You also need to set the client credentials as explained [previously](#to-run-this-project).

Execute in command line `sh exec.sh` to run the Docker in Linux, or `.\exec.ps1` to run the Docker in Windows.
