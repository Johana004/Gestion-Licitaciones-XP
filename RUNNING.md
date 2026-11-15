

<!-- local-development:start -->
# Desarrollo local

Puedes abrir este repositorio en VS Code, Antigravity o Sublime Text. El compilador y los servicios necesarios dependen del componente indicado abajo.

## .NET SDK

Instala el SDK correspondiente al destino de la tabla. .NET 8 y 9 finalizaron soporte el 2026-11-10; compilar con ellos no acredita un runtime con soporte para producción. .NET 10 requiere Visual Studio 2026 para trabajar desde ese IDE; la CLI puede usarse desde VS Code.

```powershell
dotnet --list-sdks
dotnet build 'src/Licitaciones.Api/Licitaciones.Api.csproj'
```

[.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) · [SDK / Visual Studio compatibility](https://learn.microsoft.com/en-us/dotnet/core/porting/versioning-sdk-msbuild-vs)

## Componentes y validación

Inventario y compilaciones locales del 2026-11-15. Una compilación correcta no acredita todavía interacción, compatibilidad de base de datos, instalador ni despliegue.

| Componente | Tipo / destino | Validación |
| --- | --- | --- |
| [GestionLicitaciones.slnx](<GestionLicitaciones.slnx>) | solution | Sin ejecutar |
| [src/Licitaciones.Api/Licitaciones.Api.csproj](<src/Licitaciones.Api/Licitaciones.Api.csproj>) | dotnet / net9.0 | Compila; prueba funcional pendiente |
| [src/Licitaciones.Application/Licitaciones.Application.csproj](<src/Licitaciones.Application/Licitaciones.Application.csproj>) | dotnet / net9.0 | Compila; prueba funcional pendiente |
| [src/Licitaciones.Domain/Licitaciones.Domain.csproj](<src/Licitaciones.Domain/Licitaciones.Domain.csproj>) | dotnet / net9.0 | Compila; prueba funcional pendiente |
| [src/Licitaciones.Infrastructure/Licitaciones.Infrastructure.csproj](<src/Licitaciones.Infrastructure/Licitaciones.Infrastructure.csproj>) | dotnet / net9.0 | Compila; prueba funcional pendiente |
| [src/Licitaciones.Web/Licitaciones.Web.csproj](<src/Licitaciones.Web/Licitaciones.Web.csproj>) | dotnet / net9.0 | Compila; prueba funcional pendiente |
| [src/Licitaciones.slnx](<src/Licitaciones.slnx>) | solution | Sin ejecutar |
| [tests/Licitaciones.FunctionalTests/Licitaciones.FunctionalTests.csproj](<tests/Licitaciones.FunctionalTests/Licitaciones.FunctionalTests.csproj>) | dotnet / net9.0 | Compila; prueba funcional pendiente |
| [tests/Licitaciones.IntegrationTests/Licitaciones.IntegrationTests.csproj](<tests/Licitaciones.IntegrationTests/Licitaciones.IntegrationTests.csproj>) | dotnet / net10.0 | Bloqueado: NETSDK1045 |
| [tests/Licitaciones.UnitTests/Licitaciones.UnitTests.csproj](<tests/Licitaciones.UnitTests/Licitaciones.UnitTests.csproj>) | dotnet / net9.0 | Compila; prueba funcional pendiente |

## Archivos ignorados

`.gitignore` excluye credenciales locales, estado del editor y salidas de las herramientas detectadas. No deja de versionar archivos que ya estaban en Git. Los manifiestos, archivos de bloqueo, recursos fuente y scripts de esquema forman parte del código.
<!-- local-development:end -->

<!-- verified-builds-20261115:start -->
## Compilación comprobada

- Compilación satisfactoria registrada para `src/Licitaciones.Api/Licitaciones.Api.csproj`.
- Compilación satisfactoria registrada para `src/Licitaciones.Application/Licitaciones.Application.csproj`.
- Compilación satisfactoria registrada para `src/Licitaciones.Domain/Licitaciones.Domain.csproj`.
- Compilación satisfactoria registrada para `src/Licitaciones.Infrastructure/Licitaciones.Infrastructure.csproj`.
- Compilación satisfactoria registrada para `src/Licitaciones.Web/Licitaciones.Web.csproj`.
- Compilación satisfactoria registrada para `tests/Licitaciones.FunctionalTests/Licitaciones.FunctionalTests.csproj`.
- Compilación satisfactoria registrada para `tests/Licitaciones.IntegrationTests/Licitaciones.IntegrationTests.csproj`.
- Compilación satisfactoria registrada para `tests/Licitaciones.UnitTests/Licitaciones.UnitTests.csproj`.

Comandos:

```powershell
msbuild "src/Licitaciones.Api/Licitaciones.Api.csproj" /restore /t:Build
msbuild "src/Licitaciones.Application/Licitaciones.Application.csproj" /restore /t:Build
msbuild "src/Licitaciones.Domain/Licitaciones.Domain.csproj" /restore /t:Build
msbuild "src/Licitaciones.Infrastructure/Licitaciones.Infrastructure.csproj" /restore /t:Build
msbuild "src/Licitaciones.Web/Licitaciones.Web.csproj" /restore /t:Build
msbuild "tests/Licitaciones.FunctionalTests/Licitaciones.FunctionalTests.csproj" /restore /t:Build
msbuild "tests/Licitaciones.IntegrationTests/Licitaciones.IntegrationTests.csproj" /restore /t:Build
msbuild "tests/Licitaciones.UnitTests/Licitaciones.UnitTests.csproj" /restore /t:Build
```

La compilación no acredita todos los flujos gráficos, conexiones a bases de datos ni servicios externos. No se ejecutaron operaciones de administración contra esos servicios.
<!-- verified-builds-20261115:end -->
