#!/usr/bin/env bash
docker build -t auth0-aspnetcore-01-rs256 .
docker run -p 5000:5000 -e "ASPNETCORE_URLS=http://*:5000" -it auth0-aspnetcore-01-rs256
