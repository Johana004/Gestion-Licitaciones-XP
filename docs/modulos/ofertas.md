# Módulo de Ofertas

Gestiona la recepción, almacenamiento y evaluación de las propuestas presentadas por los proveedores.

---

## 1. Responsabilidades

* Registro de ofertas enviadas por los oferentes para una licitación específica.
* Almacenamiento del monto ofertado y tiempos de entrega propuestos.
* Validación de plazos límite de entrega antes del cierre de la licitación.

---

## 2. Entidades Principales

* **`Oferta`**: ID, LicitacionId, ProveedorId, MontoOfertado, FechaPresentacion, EstadoOferta.
* **`DocumentoOferta`**: ID, OfertaId, RutaArchivo, TipoDocumento.

---

## 3. Endpoints Principales

* `POST /api/ofertas`: Registrar oferta para una licitación.
* `GET /api/licitaciones/{licitacionId}/ofertas`: Consultar ofertas recibidas.