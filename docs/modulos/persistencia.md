# Módulo de Persistencia

Define la capa de acceso a datos respaldada por PostgreSQL y Entity Framework Core.

---

## 1. Responsabilidades

* Configuración de `ApplicationDbContext` y mapeo de entidades mediante *Fluent API*.
* Ejecución automatizada de migraciones al iniciar el contenedor.
* Gestión del sembrado inicial de datos (*Seed Data*).

---

## 2. Configuración Clave

* **ORM:** Entity Framework Core 9.
* **Base de Datos:** PostgreSQL en contenedor orquestado por Kubernetes.
* **Estrategia:** Migraciones basadas en código (*Code-First*).