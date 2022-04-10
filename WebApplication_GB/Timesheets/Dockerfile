FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Timesheets/Timesheets.csproj", "Timesheets/"]
RUN dotnet restore "Timesheets/Timesheets.csproj"
COPY . .
WORKDIR "/src/Timesheets"
RUN dotnet build "Timesheets.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Timesheets.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Timesheets.dll"]
