# Documento de Visión y Alcance

**Proyecto:** Gestión Licitaciones XP  
**Metodología:** Extreme Programming (XP)  

---

## 1. Visión General del Producto

### 1.1. Declaración del Problema
La gestión manual o descentralizada de licitaciones públicas y privadas genera inconsistencias en la información, retrasos en el seguimiento de ofertas y riesgos de incumplimiento en plazos reglamentarios.

### 1.2. Declaración de Visión
Brindar una plataforma backend robusta, escalable y containerizada que centralice, organice y automatice el flujo de vida de las licitaciones, permitiendo a los equipos de gestión consultar, registrar y evaluar procesos licitatorios de forma ágil y segura.

---

## 2. Alcance del Proyecto

### 2.1. Funcionalidades Incluidas (En Alcance)
* **Gestión de Licitaciones:** CRUD completo para el registro, edición, consulta y cambio de estados en las licitaciones.
* **Módulo de Proveedores/Oferentes:** Mapeo y asociación de oferentes a procesos licitatorios específicos.
* **Persistencia Relacional:** Modelado e implementación de base de datos relacional PostgreSQL con migraciones automáticas mediante Entity Framework Core.
* **Documentación Interactiva:** Exposición de endpoints REST mediante la interfaz OpenAPI/Scalar.
* **Infraestructura y Orquestación:** Empaquetado en Docker y despliegue orquestado en Kubernetes (Deployment, Service, Secrets y PVC).

### 2.2. Funcionalidades Excluidas (Fuera de Alcance)
* Módulo de firma digital de documentos en línea.
* Pasarela de pagos para adjudicaciones.
* Notificaciones automáticas por correo masivo (planeadas para fases futuras).

---

## 3. Objetivos del Negocio y Criterios de Éxito

| Objetivo | Criterio de Éxito |
| :--- | :--- |
| **Estabilidad de Infraestructura** | Despliegue en Kubernetes en estado `1/1 Running` con persistencia de datos. |
| **Tiempo de Respuesta** | Consultas a endpoints principales en menos de 200 ms en entornos locales. |
| **Documentación de API** | Cobertura del 100% de endpoints expuestos y probables mediante Scalar UI. |

---

## 4. Riesgos y Supuestos

* **Supuesto:** Se asume un entorno de ejecución containerizado respaldado por Kubernetes (Docker Desktop / minikube).
* **Riesgo:** Incompatibilidad en ejecuciones concurrentes de migraciones de base de datos (Mitigado limitando el deployment a 1 réplica en inicialización).