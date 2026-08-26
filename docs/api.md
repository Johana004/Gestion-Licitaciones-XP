# Documentación de la API REST

Este documento especifica la arquitectura, convenciones, formatos de respuesta y endpoints expuestos por el servicio backend de **Gestión Licitaciones XP**.

---

## 1. Información General

* **Protocolo:** HTTP / HTTPS
* **Formato de Datos:** JSON (`application/json`)
* **Arquitectura:** RESTful API basada en ASP.NET Core (.NET 9)
* **Documentación Interactiva:** Expuesta mediante **Scalar API Reference** en la ruta `/scalar/v1`
* **Especificación OpenAPI:** Disponible en `/openapi/v1.json`

---

## 2. Convenciones de Codificación y Respuestas HTTP

La API utiliza los códigos de estado HTTP estándar para indicar el resultado de cada solicitud:

| Código HTTP | Descripción |
| :--- | :--- |
| **`200 OK`** | Solicitud exitosa con retorno de datos. |
| **`201 Created`** | Recurso creado exitosamente (devuelve el objeto con su ID). |
| **`400 Bad Request`** | Error de validación en el modelo o datos de entrada. |
| **`404 Not Found`** | El recurso o la ruta solicitada no existe. |
| **`500 Internal Error`** | Error no controlado en el servidor o fallo de conexión a la BD. |

---

## 3. Principales Endpoints

### 3.1. Módulo de Licitaciones (`/api/licitaciones`)

* **`GET /api/licitaciones`**  
  Obtiene el listado completo de licitaciones registradas.
* **`GET /api/licitaciones/{id}`**  
  Obtiene el detalle de una licitación específica por su identificador.
* **`POST /api/licitaciones`**  
  Registra una nueva licitación en la base de datos.
* **`PUT /api/licitaciones/{id}`**  
  Actualiza los datos o el estado de una licitación existente.
* **`DELETE /api/licitaciones/{id}`**  
  Elimina o desactiva un registro de licitación.

### 3.2. Módulo de Proveedores / Oferentes (`/api/proveedores`)

* **`GET /api/proveedores`**  
  Consulta la lista de proveedores registrados.
* **`POST /api/proveedores`**  
  Crea un nuevo proveedor en el sistema.

---

## 4. Ejemplo de Estructura de Datos (Payload JSON)

### Crear Licitación (`POST /api/licitaciones`)

```json
{
  "titulo": "Adquisición de Equipos de Cómputo 2026",
  "descripcion": "Licitación pública para la compra de estaciones de trabajo y servidores.",
  "presupuesto": 45000000.00,
  "fechaApertura": "2026-09-01T08:00:00Z",
  "fechaCierre": "2026-09-30T17:00:00Z",
  "estado": "Abierta"
}