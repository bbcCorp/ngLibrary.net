# Dependencies

For this application I will be using 

* ASP.NET Core 2.0.5 
* npm - To install various dependencies
* Angular (Angular 2)
* gulp


# Project Structure
Let's lay out a simple project structure. 

We will have 4 projects to start with 

* `ngLibrary.Core`  :   That will host key functionalities of the application
* `ngLibrary.Model`:   This will be the project that will hold the model class
* `ngLibrary.Data`  :   That project will hold that code that interacts with the database
* `ngLibrary.Web`   :   This will be the primary web application. AngularJS will be used for the frontend and Asp.NET WebAPI will be used for the backend

Additionally, we will need a Test project to test Core libraries, data libraries and the web apis
* `ngLibrary.Test` :   This will be the project that will be used for testing  

## Initializing project from standard template
So let's get started. We will use standard templates to bootstrap the application. 

You can read more about the dotnet new command [here](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore2x)

```
$ dotnet new classlib -o ngLibrary.Model
$ dotnet new classlib -o ngLibrary.Core
$ dotnet new classlib -o ngLibrary.Data
$ dotnet new xunit -o ngLibrary.Test
$ dotnet new webapi -o ngLibrary.Web
```

That will lay out the necessary project structure. 

## Creating a solution file and adding the projects
Let's create a solution file
```
$ dotnet new sln --name ngLibrary.sln
```

Now we will add the projects
```
$ dotnet sln ngLibrary.sln add ./ngLibrary.Model/ngLibrary.Model.csproj 
Project `ngLibrary.Model/ngLibrary.Model.csproj` added to the solution.

$ dotnet sln ngLibrary.sln add ./ngLibrary.Data/ngLibrary.Data.csproj
Project `ngLibrary.Data/ngLibrary.Data.csproj` added to the solution.

$ dotnet sln ngLibrary.sln add ./ngLibrary.Core/ngLibrary.Core.csproj 
Project `ngLibrary.Core/ngLibrary.Core.csproj` added to the solution.

$ dotnet sln ngLibrary.sln add ./ngLibrary.Test/ngLibrary.Test.csproj 
Project `ngLibrary.Test/ngLibrary.Test.csproj` added to the solution.

$ dotnet sln ngLibrary.sln add ./ngLibrary.Web/ngLibrary.Web.csproj 
Project `ngLibrary.Web/ngLibrary.Web.csproj` added to the solution.
```

Install all the dependencies in the Web application
```
$ cd ngLibrary.Web/
$ dotnet restore
$ npm install
```
Our next set of activity would create a few WebAPIs needed for this application. 

```
$ cd ngLibrary.Web/

$ dotnet build
...

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:25.58
$ dotnet run
...
Hosting environment: Production
Content root path: /Volumes/Mac_HDD/Users/bbc/progs/ngLibrary.net/ngLibrary.Web
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
```

Now the application is running. We can verify that we get back some information by making a GET request for the books API
```
$ curl localhost:5000/api/books
["book1","book2"]
```

## Setting up the Angular project

Make sure you have the latest version of node and npm installed. All our dependencies will be installed using npm.

First install Angular CLI 
$ npm install -g @angular/cli@latest

Once this is installed, verify that the linkage is done by issuing the following command
```
$ ng -v
...
Angular CLI: 1.7.0
Node: 9.5.0
OS: darwin x64
Angular: 
...
```

Install Gulp
```
$ npm install -g gulp
```

Next, let's create an angular project inside the ngLibrary.Web folder using the ng new command
```
$ cd ngLibrary.Web
$ ng new ClientApp
...
added 1348 packages in 102.256s
Project 'ClientApp' successfully created.
```

That will initialize the Angular project. Verify the setup using the ng serve command
```
$ cd ClientApp 
$ ng build
$ ng serve
...
** NG Live Development Server is listening on localhost:4200, open your browser on http://localhost:4200/ **
```

That's it. We have angular up and running.


## Integrating Angular application with the ASPNET CORE application

### Step 1: Make angular files available to aspnet core
Open up the `.angular-cli.json` file that is located inside the ClientApp folder.

Lookup for the section
```
"apps": [
    {
      "root": "src",
      "outDir": "dist",
      "assets": [
        "assets",
        "favicon.ico"
      ],

```
We will ensure that the output of the angular application is the wwwroot folder located in the ngLibrary.Web project.

### Step 2: Make sure that the Aspnet Core Web application serves up Static files

Open up the Startup.cs file and add the following lines to make sure that default and static files are served from the wwwroot folder

```
app.UseDefaultFiles();
app.UseStaticFiles();
```

In this application, we will use a different approach. We will segregate the Angular client and Aspnetcore server applciations and host them using Docker containers.