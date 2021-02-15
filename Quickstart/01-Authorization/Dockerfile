#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 3010

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["WebAPIApplication.csproj", ""]
RUN dotnet restore "./WebAPIApplication.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebAPIApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPIApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPIApplication.dll"]