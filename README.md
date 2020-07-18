# JournalMd Server

## Build Info

* dotnet sdk (3.1)

## Run the server

```sh
dotnet run
```

## Docker

### Build the container

Create the `appsettings.Production.json` file with `secret` or change `appsettings.json`.

```sh
docker build -t journalmd-server .
```

### Run the container

```sh
docker run -it -p 8500:80 --rm journalmd-server
```

Access the application on [localhost:8500/api/](http://localhost:8500/api/). Test using cURL.

```sh
curl -L -X POST 'http://localhost:8500/api/Users/authenticate' \
-H 'Content-Type: application/json' \
-H 'Content-Type: text/plain' \
--data-raw '{
  "username": "1",
  "password": "12345678"
}
'
```

## Development

For Visual Studio Code the `launch.json` is already set up.

### Install postman for API testing

```sh
sudo snap install postman
```

### Code geneator

```sh
dotnet tool install --global dotnet-aspnet-codegenerator
```

```sh
dotnet aspnet-codegenerator controller -name NewNameController -async -api -m NewNameModel -dc JournalMdServerContext -outDir Controllers
# or on error:
~/.dotnet/tools/dotnet-aspnet-codegenerator controller -name NewNameController -async -api -m NewNameModel -dc JournalMdServerContext -outDir Controllers
```

### DB

```
dotnet ef database update
```

### migrations

```
dotnet ef migrations add InitialCreate
```

or 

```
Add-Migration InitialCreate
```

MySQL, MS SQL Server annotations needs to be added manually as there is only one DbContext!

## Resources used

* [Tutorial: Create a web API with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio-code)
* [Get started with Swashbuckle and ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio)
* [Overview of ASP.NET Core Security](https://docs.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-3.1)
* [Welcome to IdentityServer4 (ASP.NET Core 3.x)](http://docs.identityserver.io/en/latest/index.html)
* [Protecting APIs](http://docs.identityserver.io/en/latest/topics/apis.html)

* [proudmonkey/ApiBoilerPlate](https://github.com/proudmonkey/ApiBoilerPlate) - 2 Articles attached! - asp api part
* [ASP.NET Boilerplate](https://github.com/aspnetboilerplate/aspnetboilerplate/tree/dev/src)
* [The Repository Pattern isn’t an Anti-Pattern; You’re just doing it wrong](https://brianbu.com/2019/09/25/the-repository-pattern-isnt-an-anti-pattern-youre-just-doing-it-wrong/)
  * [No need for repositories and unit of work with Entity Framework Core](https://gunnarpeipman.com/ef-core-repository-unit-of-work/)
  * This is easier but not so nice as Brians approach. Still using this and stick to tight bundling with EF. This won't be a super large enterprise solution.
* [ASP.NET Core 3.1 - JWT Authentication Tutorial with Example API](https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api)
* [Paging](https://gunnarpeipman.com/ef-core-paging/)

* [PomeloFoundation/Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
