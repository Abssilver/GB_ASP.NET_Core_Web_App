FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Authentication.BusinessLayer.Abstractions/Authentication.BusinessLayer.Abstractions.csproj", "Authentication.BusinessLayer.Abstractions/"]
RUN dotnet restore "Authentication.BusinessLayer.Abstractions/Authentication.BusinessLayer.Abstractions.csproj"
COPY . .
WORKDIR "/src/Authentication.BusinessLayer.Abstractions"
RUN dotnet build "Authentication.BusinessLayer.Abstractions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.BusinessLayer.Abstractions.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.BusinessLayer.Abstractions.dll"]
