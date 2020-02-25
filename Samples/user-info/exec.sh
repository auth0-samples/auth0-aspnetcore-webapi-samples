#!/usr/bin/env bash
docker build -t auth0-aspnetcore-user-info .
docker run --rm -p 3010:80 --name auth0-aspnetcore-user-info -it auth0-aspnetcore-user-info
