# Módulo de Tipo de Cambio

Módulo encargado de la conversión monetaria para licitaciones multimoneda.

---

## 1. Responsabilidades

* Consulta e integración de tasas de cambio vigentes.
* Normalización de montos en moneda local para evaluación comparativa de ofertas.
* Histórico de variaciones de tipo de cambio.

---

## 2. Entidades Principales

* **`TipoCambio`**: ID, MonedaOrigen, MonedaDestino, TasaCompra, TasaVenta, Fecha.