# Bitácora de Desarrollo Metodológico (Extreme Programming - XP)

Este documento registra las actividades, artefactos y aplicación de las prácticas de **Extreme Programming (XP)** durante el ciclo de vida del proyecto **Gestión Licitaciones XP**.

---

## 1. Valores y Prácticas XP Aplicadas

* **Comunicación Continua:** Retroalimentación rápida en la integración entre la capa de datos, el backend .NET y los manifiestos de Kubernetes.
* **Simplicidad (KISS):** Diseño de manifiestos minimalistas, enfocados en levantar la infraestructura esencial (1 réplica, Secrets explícitos, puertos mapeados).
* **Retroalimentación Frecuente (Feedback Loops):** Validación inmediata del comportamiento del clúster mediante `kubectl get pods`, revisión de logs en tiempo real y pruebas de endpoints HTTP.
* **Refactorización Continua:** Optimización progresiva de manifiestos YAML (ajuste de sondas, inyección de variables de entorno) e iteración sobre la configuración del pipeline en `Program.cs`.

---

## 2. Registro de Iteraciones / Sprints

### Iteración 1: Definición de Arquitectura y Persistencia
* **Objetivo:** Modelar la base de datos relacional y configurar el contenedor de PostgreSQL.
* **Entregables:**
  * Modelado del esquema en Entity Framework Core.
  * Definición de `postgres-0` con volumen persistente (PVC) en Kubernetes.
  * Configuración de `app-secret` para manejo de cadenas de conexión.

### Iteración 2: Desarrollo del Backend y API
* **Objetivo:** Implementar los controladores REST y la documentación OpenAPI / Scalar.
* **Entregables:**
  * Endpoints CRUD de gestión de licitaciones y proveedores.
  * Integración de `Scalar.AspNetCore` en `Program.cs`.
  * Pruebas de integración locales.

### Iteración 3: Containerización y Orquestación en Kubernetes
* **Objetivo:** Empaquetar la aplicación en Docker y orquestar el despliegue en Kubernetes.
* **Entregables:**
  * Creación del `Dockerfile` optimizado en múltiples etapas (*multi-stage build*).
  * Creación e integración del manifiesto `k8s/app-deployment.yaml`.
  * Configuración de `ASPNETCORE_ENVIRONMENT=Development` para habilitar la interfaz Scalar en el clúster.

---

## 3. Registro de Lecciones Aprendidas

1. **Gestión de Concurrencia en Startup:** En entornos con migraciones automáticas de base de datos (EF Core), es indispensable limitar las réplicas iniciales (`replicas: 1`) para evitar *deadlocks* en PostgreSQL.
2. **Configuración Contextual de Entornos:** Las herramientas de documentación en .NET (Scalar/Swagger) dependen del entorno de ejecución; asegurar que Kubernetes refleje el entorno adecuado (`Development`) evita bloqueos por errores HTTP 404.