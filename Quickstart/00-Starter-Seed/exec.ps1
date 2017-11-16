docker build -t auth0-aspnetcore-00-rs256 .
docker run -p 5000:5000 -e "ASPNETCORE_URLS=http://*:5000" -it auth0-aspnetcore-00-rs256
