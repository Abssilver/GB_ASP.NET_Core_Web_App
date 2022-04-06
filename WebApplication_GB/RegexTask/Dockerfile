FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RegexTask/RegexTask.csproj", "RegexTask/"]
RUN dotnet restore "RegexTask/RegexTask.csproj"
COPY . .
WORKDIR "/src/RegexTask"
RUN dotnet build "RegexTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RegexTask.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RegexTask.dll"]
