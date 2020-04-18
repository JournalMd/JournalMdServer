# JournalMd Server

## Build Info

* dotnet sdk (3.1)

## Development

```sh
sudo snap install postman

dotnet tool install --global dotnet-aspnet-codegenerator

```

```sh
dotnet aspnet-codegenerator controller -name NewNameController -async -api -m NewNameModel -dc DayLifeServerContext -outDir Controllers
# or on error:
~/.dotnet/tools/dotnet-aspnet-codegenerator controller -name NewNameController -async -api -m NewNameModel -dc DayLifeServerContext -outDir Controllers
```

## DB

```
dotnet ef database update
```

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
