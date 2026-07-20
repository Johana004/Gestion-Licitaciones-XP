 Plan de Liberación (Release Plan) e Iteraciones XP

Este documento detalla la planificación y distribución del esfuerzo para el desarrollo del Sistema de Gestión de Licitaciones, utilizando el enfoque incremental y disciplinado de Extreme Programming (XP)

 Reglas de Trabajo XP (Estándares del Equipo)
1. Planificación basada en Historias: Todo el desarrollo está guiado por Historias de Usuario priorizadas por el cliente.
2. Ciclo TDD Obligatorio: No se escribe código de producción sin antes tener una prueba automatizada que falle (Rojo-Verde-Refactor)
3. Diseño Simple: Se implementa estrictamente lo necesario para satisfacer las historias de la iteración actual
4. Integración Continua (CI): Cada commit se integrará y validará mediante GitHub Actions


 Cronograma de Iteraciones

Hemos establecido 3 iteraciones de duración uniforme para garantizar un ritmo sostenible de desarrollo:

 Iteración 1: El Núcleo del Dominio y Configuración Base
 Enfoque: Establecer los cimientos del sistema, el almacenamiento relacional de soporte y los primeros flujos del dominio

 Historias de Usuario Incluidas:
   HU-01: Gestión de Proveedores con nombres normalizados y únicos
   HU-02: Configuración de Tipo de Cambio local (CRC/USD)
 Entregable: CRUD funcional de Proveedores y Tipos de Cambio expuestos a través de la API REST e integrados a PostgreSQL con migraciones

Iteración 2: Licitaciones, Ofertas y Reglas de Negocio
 Enfoque: Implementación de la lógica de negocio core, flujos transaccionales y validación del ciclo de vida de los datos
 Historias de Usuario Incluidas:
   HU-03: Creación y Gestión del Ciclo de Vida de Licitaciones (Borrador -> Publicada -> Cerrada)
   HU-04: Registro y Validación de Ofertas Económicas (Límites presupuestarios y de vencimiento)
   HU-05: Cálculo de la Mejor Oferta y Clasificación de Ahorros
 Entregable: API REST robusta que maneja transacciones complejas, control de concurrencia optimista y validación de reglas en UTC

  Iteración 3: Niveles de Aprobación, Interfaz Web y Despliegue DevOps
 Enfoque: Completar las parametrizaciones secundarias, construir la experiencia de usuario (UX) interactiva y preparar el entorno de producción contenerizado.
 Historias de Usuario Incluidas:
   HU-06: Determinación Dinámica de Niveles de Aprobación por montos.
   HU-07: Interfaz Web Modular MVC con modo claro/oscuro y alternancia de moneda local.
   HU-08: Infraestructura DevOps (Docker Compose, Kubernetes y GitHub Actions)
 Entregable: Aplicación web integrada con frontend MVC, validaciones completas, cobertura del proyecto > 70% y despliegue local verificado en Kubernetes (v1.0.0)