FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Authentication.BusinessLayer/Authentication.BusinessLayer.csproj", "Authentication.BusinessLayer/"]
RUN dotnet restore "Authentication.BusinessLayer/Authentication.BusinessLayer.csproj"
COPY . .
WORKDIR "/src/Authentication.BusinessLayer"
RUN dotnet build "Authentication.BusinessLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.BusinessLayer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.BusinessLayer.dll"]
