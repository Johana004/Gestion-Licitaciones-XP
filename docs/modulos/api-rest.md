# Módulo API REST

Capa de exposición HTTP que centraliza los controladores y la documentación gráfica del sistema.

---

## 1. Responsabilidades

* Manejo de peticiones HTTP, parseo de JSON y validación de DTOs.
* Inyección de dependencias de los servicios de negocio.
* Exposición de especificación OpenAPI y documentación interactiva mediante **Scalar API Reference** (`/scalar/v1`).