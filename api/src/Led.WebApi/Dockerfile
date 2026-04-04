# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Led.WebApi/Led.WebApi.csproj", "src/Led.WebApi/"]
COPY ["src/Led.Infrastructure/Led.Infrastructure.csproj", "src/Led.Infrastructure/"]
COPY ["src/Led.Application/Led.Application.csproj", "src/Led.Application/"]
COPY ["src/Led.Domain/Led.Domain.csproj", "src/Led.Domain/"]
COPY ["src/Led.SharedKernal/Led.SharedKernal.csproj", "src/Led.SharedKernal/"]
RUN dotnet restore "./src/Led.WebApi/Led.WebApi.csproj" -r linux-arm64
COPY . .
WORKDIR "/src/src/Led.WebApi"
RUN dotnet build "./Led.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build -r linux-arm64 --self-contained false

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Led.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false -r linux-arm64 --no-restore --self-contained false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Led.WebApi.dll"]