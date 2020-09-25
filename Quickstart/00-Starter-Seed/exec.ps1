docker build -t auth0-aspnetcore-00-rs256 .
docker run --rm -p 3010:3010 --name auth0-aspnetcore-00-rs256 -it auth0-aspnetcore-00-rs256
