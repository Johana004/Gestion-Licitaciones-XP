
---

```markdown
# Integración de Módulos y Arquitectura de Componentes

Este documento describe el flujo de integración, desacoplamiento y comunicación entre los diferentes módulos que conforman el sistema **Gestión Licitaciones XP**.

---

## 1. Diagrama de Arquitectura de Integración

La aplicación sigue una arquitectura en capas desacopladas que interactúan a través de contratos de servicios y DTOs (Data Transfer Objects):


```

[ Cliente HTTP / Scalar UI ]
│
▼
┌────────────────────────────────────────────────────────┐
│               Capa de Exposición (API)                 │
│  - LicitacionesController   - ProveedoresController    │
└───────────────────────────┬────────────────────────────┘
│
▼
┌────────────────────────────────────────────────────────┐
│             Capa de Servicios y Negocio                │
│  - LicitacionService        - ProveedorService         │
└───────────────────────────┬────────────────────────────┘
│
▼
┌────────────────────────────────────────────────────────┐
│            Capa de Acceso a Datos (ORM)                │
│  - DbContext (EF Core)      - Migraciones y Seeds      │
└───────────────────────────┬────────────────────────────┘
│
▼
┌────────────────────────────────────────────────────────┐
│         Infraestructura de Persistencia                │
│               - PostgreSQL (Pod K8s)                   │
└────────────────────────────────────────────────────────┘

```

---

## 2. Puntos de Integración Clave

### 2.1. Módulo de Licitaciones ↔ Módulo de Proveedores
* **Relación:** Una licitación puede recibir múltiples ofertas registradas por distintos proveedores (relación 1:N / N:M).
* **Mapeo:** La asociación se valida mediante la capa de servicio asegurando que el identificador del proveedor exista antes de vincular una oferta a la licitación.

### 2.2. Backend ASP.NET Core ↔ PostgreSQL (Entity Framework Core)
* **Conexión:** Inyección de dependencia de `DbContext` utilizando la cadena de conexión inyectada desde el Secret de Kubernetes (`app-secret`).
* **Migraciones:** Inicialización automatizada al arrancar la aplicación (`Database.Migrate()`), respaldada por el contenedor `postgres-0`.

### 2.3. Pipeline HTTP ↔ Interfaz de Documentación (Scalar)
* **Middleware:** Integración de `Scalar.AspNetCore` que lee dinámicamente el esquema generado por OpenAPI (`/openapi/v1.json`) y expone la interfaz cliente interactiva.

---

## 3. Matriz de Flujo de Datos

| Flujo de Origen | Evento / Solicitud | Modulo Destino | Tipo de Comunicación |
| :--- | :--- | :--- | :--- |
| **Scalar UI / Cliente** | `POST /api/licitaciones` | `LicitacionesController` | HTTP Async (JSON) |
| **`LicitacionesController`** | Procesar Creación | `LicitacionService` | Inyección de Dependencias |
| **`LicitacionService`** | Persistir Entidad | `ApplicationDbContext` | LINQ / EF Core |
| **`ApplicationDbContext`** | SQL Insert | PostgreSQL (`postgres-0`) | TCP/IP (Puerto 5432) |

---

## 4. Estrategia de Resiliencia y Manejo de Errores
* **Manejo de Excepciones:** Middleware global (`UseExceptionHandler`) para capturar fallos no controlados y retornar respuestas estandarizadas `500 Internal Server Error`.
* **Transacciones de BD:** Manejo de transacciones en operaciones compuestas (ej. registrar licitación junto con sus oferentes iniciales) para garantizar atomicidad (ACID).

```