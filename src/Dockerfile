FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
# COPY dotnet-sqlserver-example.sln .
COPY dotnet-sqlserver-example/dotnet-sqlserver-example.csproj ./src/
RUN dotnet restore ./src/dotnet-sqlserver-example.csproj

# copy everything else and build app
COPY dotnet-sqlserver-example/. ./src/
WORKDIR /app/src
RUN dotnet publish dotnet-sqlserver-example.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/src/out ./
ENTRYPOINT ["dotnet", "dotnet-sqlserver-example.dll"]
