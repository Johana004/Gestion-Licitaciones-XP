# Registro de Uso de Inteligencia Artificial (AI-Assisted Engineering)

Este documento registra la metódica y aplicación de Inteligencia Artificial (IA) como copiloto de desarrollo durante las fases de arquitectura, depuración de código, contenedorización y orquestación del proyecto **Gestión Licitaciones XP**.

---

## 1. Declaración de Propósito

El uso de la IA en este proyecto se centró en acelerar la resolución de errores complejos de infraestructura, validar sintaxis de manifiestos y optimizar el pipeline de despliegue en Kubernetes, manteniendo el control humano sobre la arquitectura final y la lógica de negocio.

---

## 2. Áreas de Aplicación e Interacciones

| Área | Casos de Uso / Tareas Asistidas | Resultado / Impacto |
| :--- | :--- | :--- |
| **Infraestructura (Kubernetes)** | Diagnóstico y corrección de errores de sintaxis YAML, configuración de `imagePullPolicy` y variables de entorno. | Resolución de estados `ErrImageNeverPull` y fallos de montaje en pods. |
| **Depuración de Backend (.NET)** | Análisis del pipeline HTTP en `Program.cs` y diagnóstico de errores HTTP 404 en rutas de documentación. | Identificación de la restricción de entorno `IsDevelopment()` y activación de Scalar en clúster. |
| **Base de Datos y EF Core** | Prevención de colisiones y bloqueo de tablas durante ejecuciones concurrentes de migraciones. | Ajuste del modelo de réplicas (`replicas: 1`) para asegurar ejecuciones deterministas de *Seed Data*. |
| **Documentación Técnica** | Generación y estructuración de la documentación del proyecto (`docs/`). | Estandarización de archivos Markdown siguiendo principios de claridad técnica. |

---

## 3. Bitácora de Prompts y Soluciones Clave

* **Problema:** El Pod fallaba continuamente con `Startup probe failed (404)` en `/health`.
  * **Intervención IA:** Identificación de falta de controlador de Health Checks en .NET.
  * **Solución:** Desactivación temporal de sondas en el manifiesto para estabilizar el Pod.
* **Problema:** Conexión rechazada al intentar acceder a la UI de Scalar mediante `port-forward`.
  * **Intervención IA:** Análisis de variables de entorno y mapeo de controladores.
  * **Solución:** Inyección explícita de `ASPNETCORE_ENVIRONMENT: "Development"` en la especificación del Deployment.

---

## 4. Criterios de Validación Humana
Todas las sugerencias generadas por la IA fueron sometidas a:
1. Inspección visual y validación de sintaxis antes de su aplicación.
2. Pruebas de ejecución en PowerShell (`kubectl apply`, `kubectl port-forward`).
3. Verificación de logs en tiempo real (`kubectl logs`) y pruebas de integración cliente-servidor.