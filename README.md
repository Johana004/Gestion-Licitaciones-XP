# Gestion-Licitaciones-XP

<!-- local-development:start -->
## Desarrollo local

Consulta [RUNNING.md](RUNNING.md) para ubicar cada componente, abrirlo en su IDE, compilarlo y conocer las comprobaciones realizadas y los requisitos pendientes.
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
