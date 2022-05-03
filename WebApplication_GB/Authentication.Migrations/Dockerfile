FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Authentication.Migrations/Authentication.Migrations.csproj", "Authentication.Migrations/"]
RUN dotnet restore "Authentication.Migrations/Authentication.Migrations.csproj"
COPY . .
WORKDIR "/src/Authentication.Migrations"
RUN dotnet build "Authentication.Migrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.Migrations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.Migrations.dll"]
