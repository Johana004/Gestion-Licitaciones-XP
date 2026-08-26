# Módulo de Proveedores

Mantiene el catálogo centralizado de empresas y personas físicas habilitadas para participar en los procesos.

---

## 1. Responsabilidades

* Registro de información legal y comercial de los oferentes.
* Gestión del estado de habilitación del proveedor (Activo, Inactivo, Sancionado).
* Vinculación de contactos de referencia.

---

## 2. Entidades Principales

* **`Proveedor`**: ID, IdentificacionFiscal (RUC/CIF), RazonSocial, CorreoElectronico, Telefono, Estado.

---

## 3. Endpoints Principales

* `GET /api/proveedores`: Listado general de proveedores.
* `POST /api/proveedores`: Registrar nuevo proveedor.
* `GET /api/proveedores/{id}`: Consultar expediente del proveedor.