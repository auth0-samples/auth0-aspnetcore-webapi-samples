#!/usr/bin/env bash
docker build -t auth0-aspnetcore-01-rs256 .
docker run -p 3010:3010 -e "ASPNETCORE_URLS=http://*:3010" -it auth0-aspnetcore-01-rs256
