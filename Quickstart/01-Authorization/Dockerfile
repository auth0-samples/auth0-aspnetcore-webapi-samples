FROM microsoft/dotnet:1-sdk

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 3010/tcp

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:3010"]