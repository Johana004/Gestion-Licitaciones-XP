# Módulo de Licitaciones

Este módulo gestiona el ciclo de vida completo de los procesos licitatorios dentro del sistema.

---

## 1. Responsabilidades

* Registro, actualización y consulta de licitaciones.
* Control del flujo de estados (Borrador, Publicada, En Evaluación, Adjudicada, Desierta, Cancelada).
* Asociación de requisitos técnicos y financieros.

---

## 2. Entidades Principales

* **`Licitacion`**: ID, Título, Descripción, PresupuestoEstimado, FechaApertura, FechaCierre, EstadoId.
* **`EstadoLicitacion`**: ID, Nombre, Descripción.

---

## 3. Endpoints Principales

* `GET /api/licitaciones`: Listado con filtros paginados.
* `POST /api/licitaciones`: Crear nuevo registro.
* `GET /api/licitaciones/{id}`: Detalle completo.
* `PUT /api/licitaciones/{id}/estado`: Transición de estado.