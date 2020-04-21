# build stage
# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY *.csproj ./
RUN dotnet restore JournalMd.sln

# copy everything else and build app
COPY . ./
# RUN dotnet publish JournalMd.sln -c Release -o out --no-restore
RUN dotnet publish JournalMd.sln -c Release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "JournalMdServer.dll"]
