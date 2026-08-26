# Gestión Licitaciones XP

> Plataforma backend containerizada y orquestada para la gestión y seguimiento de procesos licitatorios, desarrollada con .NET 9, PostgreSQL y Kubernetes bajo la metodología **Extreme Programming (XP)**.

---

## 🛠️ Stack Tecnológico

* **Lenguaje & Framework:** C# / .NET 9 Web API & MVC
* **Base de Datos:** PostgreSQL con Entity Framework Core
* **Documentación de API:** Scalar API Reference & OpenAPI
* **Containerización:** Docker (*Multi-stage builds*)
* **Orquestación:** Kubernetes (`kubectl`, Deployments, Services, PVC, Secrets)
* **Metodología:** Extreme Programming (XP)

---

## 📁 Estructura del Proyecto

```text
├── docs/                   # Documentación técnica completa del proyecto
│   ├── api.md              # Especificación de Endpoints y OpenAPI
│   ├── arquitectura-general.md # Diseño en capas y flujo de datos
│   ├── bitacora-xp.md      # Registro metodológico XP y sprints
│   ├── docker.md           # Configuración de contenedores
│   ├── historias-usuario.md# Requerimientos y casos de uso XP
│   ├── integracion-modulos.md # Comunicación entre capas
│   ├── kubernetes.md       # Despliegue en K8s y troubleshooting
│   ├── modelo-datos.md     # Esquema relacional de PostgreSQL
│   ├── plan-xp.md          # Planificación e iteraciones
│   ├── pruebas.md          # Estrategia y reporte de pruebas
│   ├── uso-ia.md           # Registro de AI-Assisted Engineering
│   └── vision-alcance.md   # Alcance y objetivos del negocio
├── k8s/                    # Manifiestos de Kubernetes
│   └── app-deployment.yaml # Deployment y servicios del cluster
├── src/                    # Código fuente del proyecto .NET
└── README.md               # Documento principal