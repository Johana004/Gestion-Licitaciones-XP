# Documentación de Contenedorización — Gestión Licitaciones XP

Curso: ITI-822 Metodologías Ágiles  
Arquitectura: Clean Architecture (.NET 9) + PostgreSQL 16  
Estado: Operativo 🟢



 1. Resumen de la Configuración

La infraestructura del proyecto está contenedorizada utilizando Docker y orquestada con Docker Compose, lo que garantiza un entorno de desarrollo uniforme.

* Servicio de Base de Datos (`licitaciones-db`): PostgreSQL 16 (Alpine) ejecutándose en el puerto `5432`.
* Servicio de Aplicación (`licitaciones-api`): API REST en .NET 9 compilada mediante un `Dockerfile` multi-etapa que contempla la estructura modular de Clean Architecture (`src/Licitaciones.Api`, `src/Licitaciones.Application`, `src/Licitaciones.Domain`, `src/Licitaciones.Infrastructure`).



 2. Dockerfile (`Dockerfile`)

```dockerfile
# Etapa 1: Construcción y compilación
FROM [mcr.microsoft.com/dotnet/sdk:9.0](https://mcr.microsoft.com/dotnet/sdk:9.0) AS build
WORKDIR /src

# Copiar proyectos para restauración de dependencias
COPY ["src/Licitaciones.Api/Licitaciones.Api.csproj", "src/Licitaciones.Api/"]
COPY ["src/Licitaciones.Application/Licitaciones.Application.csproj", "src/Licitaciones.Application/"]
COPY ["src/Licitaciones.Domain/Licitaciones.Domain.csproj", "src/Licitaciones.Domain/"]
COPY ["src/Licitaciones.Infrastructure/Licitaciones.Infrastructure.csproj", "src/Licitaciones.Infrastructure/"]

# Restaurar paquetes NuGet
RUN dotnet restore "src/Licitaciones.Api/Licitaciones.Api.csproj"

# Copiar código fuente y compilar
COPY . .
WORKDIR "/src/src/Licitaciones.Api"
RUN dotnet build "Licitaciones.Api.csproj" -c Release -o /app/build

# Etapa 2: Publicación
FROM build AS publish
RUN dotnet publish "Licitaciones.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 3: Imagen final
FROM [mcr.microsoft.com/dotnet/aspnet:9.0](https://mcr.microsoft.com/dotnet/aspnet:9.0) AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Licitaciones.Api.dll"]