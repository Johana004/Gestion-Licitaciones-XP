 Plan de Liberación (Release Plan) e Iteraciones XP

[cite_start]Este documento detalla la planificación y distribución del esfuerzo para el desarrollo del **Sistema de Gestión de Licitaciones**, utilizando el enfoque incremental y disciplinado de **Extreme Programming (XP)**[cite: 14, 24, 292].

🎯 Reglas de Trabajo XP (Estándares del Equipo)
1. [cite_start]**Planificación basada en Historias:** Todo el desarrollo está guiado por Historias de Usuario priorizadas por el cliente.
2. [cite_start]**Ciclo TDD Obligatorio:** No se escribe código de producción sin antes tener una prueba automatizada que falle (Rojo-Verde-Refactor)[cite: 50, 59].
3. [cite_start]**Diseño Simple:** Se implementa estrictamente lo necesario para satisfacer las historias de la iteración actual[cite: 24, 50].
4. [cite_start]**Integración Continua (CI):** Cada commit se integrará y validará mediante GitHub Actions[cite: 50, 60].

---
🌀 Cronograma de Iteraciones

[cite_start]Hemos establecido **3 iteraciones** de duración uniforme para garantizar un ritmo sostenible de desarrollo:

 📦 Iteración 1: El Núcleo del Dominio y Configuración Base
* [cite_start]**Enfoque:** Establecer los cimientos del sistema, el almacenamiento relacional de soporte y los primeros flujos del dominio[cite: 273].
* **Historias de Usuario Incluidas:**
  * [cite_start]**HU-01:** Gestión de Proveedores con nombres normalizados y únicos[cite: 147, 243].
  * [cite_start]**HU-02:** Configuración de Tipo de Cambio local (CRC/USD)[cite: 188, 189].
* [cite_start]**Entregable:** CRUD funcional de Proveedores y Tipos de Cambio expuestos a través de la API REST e integrados a PostgreSQL con migraciones[cite: 70, 74, 233].

 📦 Iteración 2: Licitaciones, Ofertas y Reglas de Negocio
* [cite_start]**Enfoque:** Implementación de la lógica de negocio core, flujos transaccionales y validación del ciclo de vida de los datos[cite: 273, 274].
* **Historias de Usuario Incluidas:**
  * [cite_start]**HU-03:** Creación y Gestión del Ciclo de Vida de Licitaciones (Borrador -> Publicada -> Cerrada)[cite: 132].
  * [cite_start]**HU-04:** Registro y Validación de Ofertas Económicas (Límites presupuestarios y de vencimiento)[cite: 138, 142, 167].
  * [cite_start]**HU-05:** Cálculo de la Mejor Oferta y Clasificación de Ahorros[cite: 170, 173, 174, 175].
* [cite_start]**Entregable:** API REST robusta que maneja transacciones complejas, control de concurrencia optimista y validación de reglas en UTC[cite: 139, 140, 237, 238].

 📦 Iteración 3: Niveles de Aprobación, Interfaz Web y Despliegue DevOps
* [cite_start]**Enfoque:** Completar las parametrizaciones secundarias, construir la experiencia de usuario (UX) interactiva y preparar el entorno de producción contenerizado[cite: 274, 275].
* **Historias de Usuario Incluidas:**
  * [cite_start]**HU-06:** Determinación Dinámica de Niveles de Aprobación por montos[cite: 178].
  * [cite_start]**HU-07:** Interfaz Web Modular MVC con modo claro/oscuro y alternancia de moneda local[cite: 65, 66, 67, 198, 199].
  * [cite_start]**HU-08:** Infraestructura DevOps (Docker Compose, Kubernetes y GitHub Actions)[cite: 87, 98, 99, 100].
* [cite_start]**Entregable:** Aplicación web integrada con frontend MVC, validaciones completas, cobertura del proyecto > 70% y despliegue local verificado en Kubernetes (v1.0.0)[cite: 102, 255, 280, 317].