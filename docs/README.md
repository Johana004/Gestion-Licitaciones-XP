
---

# 📋 Gestión Licitaciones XP

> Sistema enterprise de gestión, evaluación y adjudicación de licitaciones y ofertas enfocado en **Clean Architecture**, **Domain-Driven Design (DDD)** y prácticas ágiles de **Extreme Programming (XP)**.

---

## 🚀 Acerca del Proyecto

**Gestión Licitaciones XP** es una solución integral desarrollada en **.NET 8** diseñada para automatizar y transparentar los procesos licitatorios corporativos e institucionales. El sistema permite gestionar desde la recepción de ofertas de proveedores hasta la evaluación multidimensional de criterios (precio, tiempo de entrega, niveles de aprobación) y la adjudicación automática de la mejor oferta.

Su arquitectura modular garantiza un desacoplamiento estricto de la lógica de negocio frente a frameworks externos, facilitando una alta testabilidad, mantenibilidad a largo plazo y la capacidad de auditar métricas de código mediante pruebas unitarias.

---

## 🏛️ Arquitectura del Sistema (Clean Architecture + DDD)

El proyecto está organizado en **4 capas principales**, respetando la regla de dependencia hacia adentro (las capas externas dependen del Dominio, pero el Dominio no conoce ningún detalle técnico o de infraestructura):

```text
               ┌─────────────────────────────────────────┐
               │         Presentation Layer             │
               │  (Licitaciones.Api / Licitaciones.Web)   │
               └────────────────────┬────────────────────┘
                                    │
                                    ▼
               ┌─────────────────────────────────────────┐
               │          Application Layer              │
               │      (Licitaciones.Application)         │
               └────────────────────┬────────────────────┘
                                    │
                                    ▼
               ┌─────────────────────────────────────────┐
               │            Domain Layer                 │
               │         (Licitaciones.Domain)           │
               └─────────────────────────────────────────┘
                                    ▲
                                    │
               ┌─────────────────────────────────────────┐
               │         Infrastructure Layer            │
               │    (Licitaciones.Infrastructure)        │
               └─────────────────────────────────────────┘

```

### **Detalle de Capas y Responsabilidades:**

1. **`Licitaciones.Domain` (Core de Negocio):**
* Contiene la lógica pura del negocio, entidades, agregados, objetos de valor y servicios de dominio.
* Totalmente libre de dependencias a bases de datos o frameworks web.


2. **`Licitaciones.Application` (Casos de Uso):**
* Orquesta la ejecución de los procesos de negocio.
* Contiene los contratos de interfaces, lógica de servicios de aplicación y la transformación a DTOs.


3. **`Licitaciones.Infrastructure` (Persistencia y Servicios Externos):**
* Implementa los repositorios, la configuración del contexto de Entity Framework Core (`LicitacionesDbContext`), la unidad de trabajo (`UnitOfWork`) y servicios técnicos (como proveedores de fecha/hora).


4. **`Licitaciones.Api` & `Licitaciones.Web` (Presentación):**
* **API:** Expone endpoints RESTful con documentación dinámica vía OpenAPI / Scalar.
* **Web:** Interfaz de usuario basada en ASP.NET Core MVC para operaciones operativas y paneles administrativos.



---

## 🛠️ Tecnologías y Herramientas

* **Runtime & Lenguaje:** .NET 8 (C# 12)
* **Patrones & Arquitectura:** Clean Architecture, DDD, Repository Pattern, Unit of Work, Dependency Injection.
* **Persistencia & ORM:** Entity Framework Core 8, PostgreSQL / SQL Server.
* **Documentación API:** Scalar API Documentation & OpenAPI Specification.
* **Testing & Calidad:** xUnit, Moq / NSubstitute, ReportGenerator, Coverlet (`XPlat Code Coverage`).

---

## 📦 Componentes y Clases Principales

| Capa | Componente / Clase | Rol y Descripción |
| --- | --- | --- |
| **Domain** | `Licitacion` | Entidad agregada principal que gestiona estados, plazos y requerimientos del proceso. |
|  | `Oferta` | Entidad que representa la propuesta técnica/económica enviada por un proveedor. |
|  | `Proveedor` | Gestiona la información corporativa y legal de los oferentes habilitados. |
|  | `TipoCambio` & `NivelAprobacion` | Entidades para cálculos multimoneda y validación de rangos jerárquicos de autorización. |
|  | `LicitacionDomainService` | Servicio de dominio encargado del algoritmo de evaluación y ponderación de ofertas. |
|  | `MejorOfertaResultado` | Objeto de valor/resultado para el reporte técnico de adjudicación. |
| **Application** | `LicitacionEvaluadorService` | Orquestador que invoca el motor de evaluación y valida las reglas antes de adjudicar. |
|  | `LicitacionService` & `OfertaService` | Gestión de ciclos de vida de licitaciones y registro de ofertas. |
|  | `ProveedorService` | Administración de catálogo de proveedores y auditoría. |
| **Infrastructure** | `LicitacionesDbContext` | Contexto principal de EF Core con mapeo explícito de entidades (`EntityTypeConfiguration`). |
|  | `UnitOfWork` | Manejo de transacciones atómicas para garantizar consistencia de datos. |
|  | `LicitacionRepository`, `OfertaRepository` | Repositorios concretos con consultas optimizadas. |
| **Api / Web** | `LicitacionesController` | Endpoints REST para creación, consulta y adjudicación de procesos. |
|  | `OfertasController` & `ProveedoresController` | Endpoints para recepción de propuestas y catálogo de proveedores. |

---

## ⚡ Guía de Instalación y Ejecución

### **Prerrequisitos:**

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Instancia local o remota de base de datos (PostgreSQL / SQL Server)
* Herramienta global de ReportGenerator instalada:
```powershell
dotnet tool install --global dotnet-reportgenerator-globaltool

```



---

### **1. Clonar el Proyecto y Restaurar Dependencias**

```powershell
# Clonar el repositorio
git clone <URL_DEL_REPOSITORIO>
cd "Gestion Licitaciones XP"

# Restaurar paquetes NuGet
dotnet restore

```

---

### **2. Configuración de Base de Datos y Migraciones**

Actualiza la cadena de conexión en el archivo `src/Licitaciones.Api/appsettings.json` y ejecuta:

```powershell
# Aplicar migraciones pendientes
dotnet ef database update --project src/Licitaciones.Infrastructure --startup-project src/Licitaciones.Api

```

---

### **3. Ejecutar la Aplicación**

```powershell
# Para iniciar la API REST con la documentación de Scalar:
dotnet run --project src/Licitaciones.Api

# Para iniciar la aplicación Web MVC:
dotnet run --project src/Licitaciones.Web

```

> **Documentación interactiva:** Al ejecutar la API, navega a `http://localhost:<puerto>/scalar/v1` para explorar y probar los endpoints REST.

---

## 📊 Pruebas Unitarias y Cobertura de Código

El proyecto implementa un pipeline de pruebas en **xUnit** diseñado bajo los principios de TDD. Para mantener una métrica de calidad realista y limpia, el reporte de cobertura excluye automáticamente el código autogenerado o plano (Migraciones de Entity Framework, Vistas MVC, DTOs y ViewModels).

### **Comandos de Ejecución de Cobertura (PowerShell):**

```powershell
# 1. Limpiar ejecuciones anteriores y ejecutar la suite de pruebas unitarias
Remove-Item -Recurse -Force tests/**/TestResults
dotnet test --collect:"XPlat Code Coverage"

# 2. Procesar y generar el reporte HTML / Resumen de Texto filtrando boilerplate
reportgenerator -reports:"tests/**/TestResults/**/coverage.cobertura.xml" `
                -targetdir:"CoverageReport" `
                -reporttypes:"TextSummary;Html" `
                -classfilters:"-*.Migrations.*;-*.Views_*;-*.DTOs.*;-*.Models.*"

# 3. Mostrar el resumen de métricas en la consola
Get-Content .\CoverageReport\Summary.txt

```

---

### **Métricas y Cobertura Objetivo:**

* **Cobertura de Línea Objetivo:** > 40% - 70% en código con lógica activa.
* **Procesos Críticos Probados:** Algoritmo de selección de mejor oferta (`LicitacionEvaluadorService`), estado de entidades de dominio (`Licitacion`, `Oferta`, `Proveedor`) y persistencia de repositorios en Infraestructura.
* **Ver Reporte Gráfico:** Abre el archivo `CoverageReport/index.html` en cualquier navegador web.

---

## 🤝 Principios Agiles y Metodología (XP)

El desarrollo del sistema aplica los valores fundamentales de **Extreme Programming (XP)**:

* **Desarrollo Guiado por Pruebas (TDD):** Redacción previa o concurrente de pruebas unitarias para validar cada caso de uso y regla de negocio.
* **Diseño Simple (Keep It Simple):** Evita sobre-ingeniería; la arquitectura evoluciona según las necesidades reales expresadas en las historias de usuario.
* **Refactorización Continua:** Mejoras constantes en la estructura del código sin alterar la funcionalidad existente, respaldadas por la suite de pruebas automatizadas.
* **Código Limpio y Auto-documentado:** Nombres descriptivos, alta cohesión y bajo acoplamiento entre componentes.

---

---
