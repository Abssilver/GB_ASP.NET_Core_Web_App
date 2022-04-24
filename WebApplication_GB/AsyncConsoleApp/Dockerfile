FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AsyncConsoleApp/AsyncConsoleApp.csproj", "AsyncConsoleApp/"]
RUN dotnet restore "AsyncConsoleApp/AsyncConsoleApp.csproj"
COPY . .
WORKDIR "/src/AsyncConsoleApp"
RUN dotnet build "AsyncConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AsyncConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AsyncConsoleApp.dll"]
