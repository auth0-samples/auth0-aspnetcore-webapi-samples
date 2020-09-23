docker build -t auth0-aspnetcore-hs256 .
docker run --rm -p 5000:5000 --name auth0-aspnetcore-hs256 -it auth0-aspnetcore-hs256
