FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["webProductos.Api/webProductos.Api.csproj", "webProductos.Api/"]
COPY ["webProductos.Domain/webProductos.Domain.csproj", "webProductos.Domain/"]
COPY ["webProductos.Application/webProductos.Application.csproj", "webProductos.Application/"]
COPY ["webProductos.Infrastructure/webProductos.Infrastructure.csproj", "webProductos.Infrastructure/"]
RUN dotnet restore "webProductos.Api/webProductos.Api.csproj"
COPY . .
WORKDIR "/src/webProductos.Api"
RUN dotnet build "./webProductos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./webProductos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webProductos.Api.dll"]
