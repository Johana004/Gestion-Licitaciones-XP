# Módulo de Niveles de Aprobación

Aplica las reglas de gobernanza y montos máximos permitidos para la autorización de adjudicaciones.

---

## 1. Responsabilidades

* Validación de rangos presupuestarios para determinar el nivel de aprobación requerido.
* Asignación de flujos de aprobación a usuarios o roles específicos según la cuantía.
* Registro de bitácora de aprobaciones/rechazos.

---

## 2. Entidades Principales

* **`NivelAprobacion`**: ID, Nombre, MontoMinimo, MontoMaximo, RolRequerido.
* **`AprobacionLicitacion`**: ID, LicitacionId, NivelAprobacionId, UsuarioId, FechaAprobacion, Estado.