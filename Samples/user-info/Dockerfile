FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /source

COPY *.csproj .
RUN ["dotnet", "restore"]

COPY . .
RUN ["dotnet", "build"]

EXPOSE 3010/tcp

ENTRYPOINT ["dotnet", "run"]
