#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IAGTO.Enforcer.Api/IAGTO.Enforcer.Api.csproj", "IAGTO.Enforcer.Api/"]
RUN dotnet restore "./IAGTO.Enforcer.Api/./IAGTO.Enforcer.Api.csproj"
COPY . .
COPY ["IAGTO.Enforcer.Api/policies", "/app/build/policies"]
WORKDIR "/src/IAGTO.Enforcer.Api"
RUN dotnet build "./IAGTO.Enforcer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IAGTO.Enforcer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/build/policies ./policies
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IAGTO.Enforcer.Api.dll"]