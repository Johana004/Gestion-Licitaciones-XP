# Estrategia y Reporte de Pruebas

Este documento describe el plan, ejecución y resultados de las pruebas realizadas sobre el sistema **Gestión Licitaciones XP** para validar su funcionalidad, persistencia y despliegue en infraestructura containerizada.

---

## 1. Niveles de Prueba

* **Pruebas Unitarias:** Validación de la lógica de dominio y reglas de negocio del servicio de licitaciones utilizando xUnit / NUnit.
* **Pruebas de Integración (Base de Datos):** Verificación del correcto funcionamiento de las migraciones de Entity Framework Core y sembrado de datos (*Seed Data*) sobre PostgreSQL.
* **Pruebas de API (HTTP / OpenAPI):** Validación del comportamiento de los controladores y respuestas REST mediante la interfaz interactiva de Scalar.
* **Pruebas de Infraestructura y Despliegue:** Confirmación de la estabilidad de los Pods y mapeo de puertos dentro del clúster de Kubernetes.

---

## 2. Casos de Prueba Ejecutados

| ID | Tipo | Descripción | Resultado |
| :--- | :--- | :--- | :--- |
| **TC-01** | Integración | Conexión e inicialización del esquema de DB en PostgreSQL desde .NET. | **Exitoso** |
| **TC-02** | Infraestructura | Despliegue de Pod `app-deployment` en Kubernetes y transición a estado `1/1 Running`. | **Exitoso** |
| **TC-03** | API / UI | Carga y renderizado de la documentación interactiva Scalar en `/scalar/v1`. | **Exitoso** |
| **TC-04** | Red / Puertos | Exposición del servicio backend mediante `port-forward` en `localhost:8080`. | **Exitoso** |

---

## 3. Matriz de Cobertura y Configuración

* **Entorno de Prueba:** Kubernetes en Docker Desktop (`ASPNETCORE_ENVIRONMENT=Development`).
* **Base de Datos:** PostgreSQL en contenedor `postgres-0`.
* **Herramientas de Verificación:** PowerShell (`Invoke-WebRequest`), navegador web y logs de Pod (`kubectl logs`).

---

## 4. Criterios de Aceptación
* El 100% de los Pods indispensables se encuentran en ejecución sin reinicios anómalos (`RESTARTS: 0`).
* Los controladores responden códigos HTTP `200 OK` para solicitudes válidas expuestas en Scalar.