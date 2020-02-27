#!/usr/bin/env bash
docker build -t auth0-aspnetcore-multiple-issuer .
docker run --rm -p 3010:80 --name auth0-aspnetcore-multiple-issuer -it auth0-aspnetcore-multiple-issuer
