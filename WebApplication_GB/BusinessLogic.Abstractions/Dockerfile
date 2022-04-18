FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BusinessLogic.Abstractions/BusinessLogic.Abstractions.csproj", "BusinessLogic.Abstractions/"]
RUN dotnet restore "BusinessLogic.Abstractions/BusinessLogic.Abstractions.csproj"
COPY . .
WORKDIR "/src/BusinessLogic.Abstractions"
RUN dotnet build "BusinessLogic.Abstractions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BusinessLogic.Abstractions.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusinessLogic.Abstractions.dll"]
