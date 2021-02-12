# Seed Project for the ASP.NET Core Web API Quickstart

This seed project can be used if you want to follow along with the steps in the [ASP.NET Core Web API Quickstart](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi).

This starter seed is a basic web API application which includes the dependencies required to use the JWT middleware.

## Run the Project

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