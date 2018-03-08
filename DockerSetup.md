# Docker setup

## Running ASPNETCORE application in a docker container

We can host the server inside a microsoft/aspnetcore docker container. 

Here's the steps to setup and run the server 
```
$ cd src/server/ngLibrary.Web/
$ docker build -t nglibrary-webapi-server .

$ docker run -it -p 5000:80 nglibrary-webapi-server
...
Hosting environment: Production
Content root path: /app
Now listening on: http://[::]:80
Application started. Press Ctrl+C to shut down.

```


## Running Angular client application in a docker container

We can host the client inside a node docker container. 

Here's the steps to setup and run the server 
```
$ cd src/client/
$ docker build -t nglibrary-webclient-server .

$ docker run -it -p 4200:4200 nglibrary-webclient-server

> client-app@0.0.0 start /ng-app
> ng serve

** NG Live Development Server is listening on 0.0.0.0:4200, open your browser on http://localhost:4200/ **

```
