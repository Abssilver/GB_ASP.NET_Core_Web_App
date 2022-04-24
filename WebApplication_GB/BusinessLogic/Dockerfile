FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
RUN dotnet restore "BusinessLogic/BusinessLogic.csproj"
COPY . .
WORKDIR "/src/BusinessLogic"
RUN dotnet build "BusinessLogic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BusinessLogic.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusinessLogic.dll"]
