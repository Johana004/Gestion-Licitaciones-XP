Arquitectura General del Sistema — Gestión Licitaciones XP

Curso: ITI-822 Metodologías Ágiles  
Proyecto Final: Extreme Programming (XP)  
Marco Arquitectónico: Clean Architecture (Arquitectura Limpia)  
Tecnología Base: .NET 9 Web API & PostgreSQL 16

 1. Resumen general

El sistema Gestión Licitaciones XP se ha elaborado con base en los fundamentos de Clean Architecture (Arquitectura Limpia), desarrollados por Robert C. Martin, también conocido como Uncle Bob. La independencia absoluta del control de frameworks externos, bases de datos e interfaces de usuario es el objetivo principal.

 Principios esenciales
 Independencia de frameworks: Las reglas y el dominio de negocio no tienen dependencia en bibliotecas externas.
 Comprobabilidad (Testability): Utilizando TDD, es posible probar la lógica de negocio sin requerir servidores web ni bases de datos.
 Independencia de la Base de Datos: En el nivel de infraestructura, PostgreSQL se desacopla mediante el patrón Repositorio e Interfaces.
 División de responsabilidades: Cada capa tiene límites definidos y reglas de dependencia rigurosas que apuntan hacia el centro.


 2. Diagrama de Capas (Mermaid)

El siguiente diagrama ilustra la dirección de las dependencias. Las capas externas conocen a las internas, pero el centro (Domain) desconoce por completo la existencia de las capas exteriores.

mermaid
graph TD
    subgraph UI_API ["Capa de Presentación / API"]
        API["Licitaciones.Api (.NET 9)"]
        WEB["Licitaciones.Web"]
    end

    subgraph INFRA ["Capa de Infraestructura"]
        INFRA_PROJ["Licitaciones.Infrastructure"]
        EF["Entity Framework Core 9"]
        PG[("PostgreSQL 16")]
    end

    subgraph APP ["Capa de Aplicación"]
        APP_PROJ["Licitaciones.Application"]
        USE_CASES["Casos de Uso / Servicios"]
        INTERFACES["Interfaces de Repositorio"]
    end

    subgraph DOMAIN ["Capa de Dominio (Núcleo)"]
        DOM_PROJ["Licitaciones.Domain"]
        ENTITIES["Entidades y Agregados"]
        VO["Objetos de Valor (Value Objects)"]
        EXCEPTIONS["Excepciones de Dominio"]
    end

    API --> APP_PROJ
    WEB --> APP_PROJ
    INFRA_PROJ --> APP_PROJ
    INFRA_PROJ --> EF
    EF --> PG
    APP_PROJ --> DOM_PROJ
    INTERFACES -. Define contrato .-> DOM_PROJ
    INFRA_PROJ -. Implementa interfaces .-> INTERFACES


   3. Descripción exhaustiva de las capas
    3.1. Capa 1: Licitaciones.Domain (núcleo)
    Objetivo: Incluir las entidades de negocio, las reglas empresariales inherentes y las validaciones del núcleo.

    Dependencias: Ninguna (0 dependencias externas).

    Componentes: Entidades (como la licitación, el usuario y la oferta), elementos de valor (por ejemplo, RUT o monto), excepciones de dominio.

    3.2. Capa 2: Licitaciones. Aplicación (lógica de aplicación)
    Objetivo: Establecer los casos de uso del sistema, los DTOs, las validaciones con FluentValidation y las interfaces para persistencia/servicios.

    Dependencias: Solo depende de Licitaciones.Domain.

    Componentes: Consultas, comandos, DTOs e interfaces (IUnitOfWork, ILicitacionRepository).

    3.3 Capa 3: Licitaciones. Infraestructura (adaptadores e infraestructura)
    Objetivo: Poner en marcha el acceso a datos utilizando Entity Framework Core, la integración con PostgreSQL, la comunicación externa y el registro.

    Dependencias: Se basa en Licitaciones.Application y Licitaciones.Domain.

    Componentes: AplicaciónDbContext, configuraciones y mapeos de Entity Framework, repositorios específicos.


    3.4. Capa 4: Licitaciones.Api (Interfaz de usuario o presentación)
    Objetivo: Presentar los puntos finales RESTful para la interacción HTTP, gestionar la autorización y autenticación JWT y ajustar el contenedor de dependencias (DI).

    Dependencias: Es dependiente de las licitaciones. Aplicación e infraestructura de las licitaciones.

    4. Aplicación de patrones de diseño
Unidad de trabajo y repositorio: Concentración y abstracción de las transacciones respecto a la base de datos.

Inyección de dependencias (DI): Inversión de control administrada de manera nativa por el contenedor de .NET 9.

DTO (Objetos de transferencia de datos): Separación entre las respuestas JSON/HTTP y el modelo de dominio.

Objeto de valor: Encapsulamiento e inmutabilidad de atributos compuestos (por ejemplo, identificaciones o valores monetarios).

