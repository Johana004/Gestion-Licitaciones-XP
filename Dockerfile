# Etapa 1: Construcción y compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar los proyectos de cada capa para restaurar dependencias de forma eficiente
COPY ["src/Licitaciones.Api/Licitaciones.Api.csproj", "src/Licitaciones.Api/"]
COPY ["src/Licitaciones.Application/Licitaciones.Application.csproj", "src/Licitaciones.Application/"]
COPY ["src/Licitaciones.Domain/Licitaciones.Domain.csproj", "src/Licitaciones.Domain/"]
COPY ["src/Licitaciones.Infrastructure/Licitaciones.Infrastructure.csproj", "src/Licitaciones.Infrastructure/"]

# Restaurar paquetes NuGet
RUN dotnet restore "src/Licitaciones.Api/Licitaciones.Api.csproj"

# Copiar todo el código fuente y compilar la API
COPY . .
WORKDIR "/src/src/Licitaciones.Api"
RUN dotnet build "Licitaciones.Api.csproj" -c Release -o /app/build

# Etapa 2: Publicación
FROM build AS publish
RUN dotnet publish "Licitaciones.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 3: Imagen final de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Licitaciones.Api.dll"]