#!/usr/bin/env bash
docker build -t auth0-aspnetcore-00-starter-seed .
docker run --rm -p 3010:3010 --name auth0-aspnetcore-00-starter-seed -it auth0-aspnetcore-00-starter-seed

