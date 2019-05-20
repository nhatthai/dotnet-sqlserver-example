FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
# using docker: we will use src.sln and src.csproj
COPY src.sln .
COPY src.csproj ./src/
RUN dotnet restore

# copy everything else and build app
COPY dotnet-sqlserver-example/. ./src/
RUN rm -rf ./src/dotnet-sqlserver-example.csproj
WORKDIR /app/src
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/src/out ./
ENTRYPOINT ["dotnet", "src.dll"]