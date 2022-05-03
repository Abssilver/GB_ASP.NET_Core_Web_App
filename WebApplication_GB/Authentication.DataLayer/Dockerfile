FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Authentication.DataLayer/Authentication.DataLayer.csproj", "Authentication.DataLayer/"]
RUN dotnet restore "Authentication.DataLayer/Authentication.DataLayer.csproj"
COPY . .
WORKDIR "/src/Authentication.DataLayer"
RUN dotnet build "Authentication.DataLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.DataLayer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.DataLayer.dll"]
