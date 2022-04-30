FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Authentication.Datalayer.Abstractions/Authentication.Datalayer.Abstractions.csproj", "Authentication.Datalayer.Abstractions/"]
RUN dotnet restore "Authentication.Datalayer.Abstractions/Authentication.Datalayer.Abstractions.csproj"
COPY . .
WORKDIR "/src/Authentication.Datalayer.Abstractions"
RUN dotnet build "Authentication.Datalayer.Abstractions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.Datalayer.Abstractions.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.Datalayer.Abstractions.dll"]
