FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HockeyPlatform.API/HockeyPlatform.API.csproj", "HockeyPlatform.API/"]
COPY ["HockeyPlatform.Persistence/HockeyPlatform.Persistence.csproj", "HockeyPlatform.Persistence/"]
COPY ["HockeyPlatform.Domain/HockeyPlatform.Domain.csproj", "HockeyPlatform.Domain/"]
RUN dotnet restore "HockeyPlatform.API/HockeyPlatform.API.csproj"
COPY . .
WORKDIR "/src/HockeyPlatform.API"
RUN dotnet build "./HockeyPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HockeyPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HockeyPlatform.API.dll"]
