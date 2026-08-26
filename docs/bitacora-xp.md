# Bitácora de Desarrollo - Extreme Programming (XP)

## Resumen General del Proceso XP
El desarrollo de la solución **Sistema de Gestión de Licitaciones XP** se llevó a cabo aplicando exclusivamente la metodología **Extreme Programming (XP)**. El proyecto se estructuró en **4 iteraciones de duración uniforme**, guiadas por el *Planning Game*, pruebas automáticas (TDD), diseño simple, refactorización continua, pequeñas liberaciones y propiedad colectiva del código[cite: 2].

---

## Iteración 1: Dominio, Persistencia y Módulo de Proveedores

* **Fechas:** Semanas 1 y 2
* **Historias de Usuario:** HU-01 (Registro de Proveedores), HU-02 (Normalización y Unicidad de Proveedores)
* **Objetivos de la Iteración:**
  * Configurar la estructura del monolito modular en .NET 9 (`Domain`, `Application`, `Infrastructure`, `Web`, `Api`)[cite: 2].
  * Diseñar la base de datos en PostgreSQL con Entity Framework Core 9[cite: 2].
  * Implementar TDD para la normalización de cadenas y reglas de unicidad de proveedores[cite: 2].
* **Ciclo TDD & Refactorización:**
  * *Rojo:* Escritura de pruebas unitarias para rechazar caracteres no válidos e ignorar espacios/mayúsculas en nombres de proveedores[cite: 2].
  * *Verde:* Implementación del servicio de dominio `ProveedorService` con expresión regular de validación[cite: 2].
  * *Refactorización:* Extracción de la lógica de normalización de cadenas a un objeto de valor reutilizable[cite: 2].
* **Velocidad XP:** 12 Puntos de Historia (Planificados: 12 / Cumplidos: 12).
* **Pequeña Liberación:** Módulo de Proveedores funcional con pruebas unitarias y de integración pasando al 100%[cite: 2].
* **Retroalimentación del Cliente:** Aprobar el formato estricto de nombres de proveedores y solicitar mensajes claros en caso de duplicados[cite: 2].

---

## Iteración 2: Licitaciones, Estados y Niveles de Aprobación

* **Fechas:** Semanas 3 y 4
* **Historias de Usuario:** HU-03 (Creación y Publicación de Licitaciones), HU-04 (Control de Estados y Fechas de Cierre), HU-05 (Niveles de Aprobación Parametrizables)
* **Objetivos de la Iteración:**
  * Implementar el ciclo de vida de licitaciones (`Borrador`, `Publicada`, `Cerrada`)[cite: 2].
  * Configurar la tabla parametrizable de niveles de aprobación según el presupuesto en CRC[cite: 2].
  * Crear la semilla de datos inicial para niveles de aprobación y tipos de cambio[cite: 2].
* **Ciclo TDD & Refactorización:**
  * *Rojo:* Pruebas para evitar la transición inválida de `Publicada` a `Borrador` y validar rangos de aprobación sin traslapes[cite: 2].
  * *Verde:* Lógica de máquina de estados en `Licitacion` y repositorio de `NivelAprobacion`[cite: 2].
  * *Refactorización:* Encapsulamiento del cambio de estado dentro de métodos con nombre explícito en la entidad de dominio[cite: 2].
* **Velocidad XP:** 16 Puntos de Historia (Planificados: 16 / Cumplidos: 16).
* **Pequeña Liberación:** API y servicios de Licitaciones y Niveles de Aprobación probados y desplegables en PostgreSQL[cite: 2].
* **Retroalimentación del Cliente:** Confirmar que el control de calendario restrinja fechas pasadas en la creación de licitaciones[cite: 2].

---

## Iteración 3: Ofertas Económicas, Clasificación y Conversión Monetaria

* **Fechas:** Semanas 5 y 6
* **Historias de Usuario:** HU-06 (Registro y Rechazo de Ofertas), HU-07 (Cálculo de Mejor Oferta y Clasificación de Ahorro), HU-08 (Visualización y Conversión CRC/USD)
* **Objetivos de la Iteración:**
  * Crear el flujo de ofertas validando que no superen el presupuesto ni se registren tras el vencimiento[cite: 2].
  * Calcular la mejor oferta, clasificación de ahorro (%) y desempate por fecha de registro[cite: 2].
  * Implementar la conversión monetaria referencial a USD usando el tipo de cambio activo[cite: 2].
* **Ciclo TDD & Refactorización:**
  * *Rojo:* Pruebas unitarias para ofertas duplicadas por proveedor, ofertas superiores al presupuesto y cálculo de porcentaje de ahorro[cite: 2].
  * *Verde:* Implementación de `OfertaService` y consultas de cálculo en la capa de aplicación[cite: 2].
  * *Refactorización:* Abstracción del reloj del sistema mediante `IDateTimeProvider` para pruebas deterministas de vencimiento[cite: 2].
* **Velocidad XP:** 18 Puntos de Historia (Planificados: 18 / Cumplidos: 18).
* **Pequeña Liberación:** Módulo completo de Ofertas e integración con el cálculo de conversión monetaria[cite: 2].
* **Retroalimentación del Cliente:** Mostrar la fecha de vigencia del tipo de cambio junto a la conversión a USD[cite: 2].

---

## Iteración 4: Interfaz Web, API REST, Docker y Despliegue en Kubernetes

* **Fechas:** Semanas 7 y 8
* **Historias de Usuario:** HU-09 (Interfaz Web MVC y Modo Oscuro), HU-10 (API REST, OpenAPI y Scalar UI), HU-11 (Contenerización y Orquestación en Kubernetes)
* **Objetivos de la Iteración:**
  * Desarrollar la Landing Page y la interfaz MVC con soporte para modo claro/oscuro y alternancia CRC/USD[cite: 2].
  * Exponer los endpoints de la API REST con `ProblemDetails` y documentación en Scalar UI (`/scalar/v1`)[cite: 2].
  * Configurar Docker Compose y desglosar los manifiestos de Kubernetes en `/k8s` con sondas `/health`[cite: 2].
  * Configurar la integración continua en GitHub Actions[cite: 2].
* **Ciclo TDD & Refactorización:**
  * *Rojo:* Pruebas de integración con Testcontainers (PostgreSQL real) y pruebas funcionales E2E con Playwright[cite: 2].
  * *Verde:* Ajuste de controladores, mapeo de rutas `/health` en `Program.cs` y configuración del pipeline de CI[cite: 1, 2].
  * *Refactorización:* Limpieza de logs, ordenamiento de manifiestos YAML y estandarización del manejo de excepciones[cite: 2].
* **Velocidad XP:** 20 Puntos de Historia (Planificados: 20 / Cumplidos: 20).
* **Pequeña Liberación:** Release final **v1.0.0** completamente validado en Kubernetes y CI/CD con GitHub Actions[cite: 1, 2].
* **Retroalimentación del Cliente:** Aprobación final del producto con todas las reglas de negocio e infraestructura validadas[cite: 2].