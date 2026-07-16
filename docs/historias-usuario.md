 Catálogo de Historias de Usuario (XP)

[cite_start]Este documento detalla las Historias de Usuario que guían el diseño, desarrollo y pruebas del sistema.

---

 📦 ITERACIÓN 1: El Núcleo del Dominio y Configuración Base

 HU-01: Registro de Proveedores Unificados
Como Encargado de Licitaciones  
Quiero registrar proveedores en el sistema mediante su nombre  
[cite_start]Para poder asociarlos posteriormente a las ofertas de las licitaciones[cite: 70].

 Estimación:3 Puntos de Historia (PH)  
 Prioridad: Alta  

Criterios de Aceptación (Verificables):
1. [cite_start]Validación de Caracteres: El nombre del proveedor solo puede contener letras, números, espacios, puntos, comas y paréntesis normales[cite: 159, 160]. [cite_start]Cualquier otro carácter especial debe ser rechazado con un error descriptivo[cite: 161, 194].
2. [cite_start]Normalización Estricta: Antes de persistir o validar duplicidad, el nombre debe eliminar espacios a los lados, reducir espacios intermedios repetidos, normalizar Unicode y compararse sin distinguir mayúsculas ni minúsculas (Ej: "Empresa Central" es idéntica a "  empresa   central  ")[cite: 147, 154, 155, 156, 157].
3. [cite_start]Unicidad en PostgreSQL: El sistema debe impedir el registro de dos proveedores con el mismo nombre normalizado mediante una validación del lado del servidor y un índice único a nivel de base de datos[cite: 148].
4. [cite_start]Borrado Lógico/Físico Seguro: Un proveedor no puede ser eliminado físicamente si ya cuenta con ofertas registradas[cite: 192]. [cite_start]Se debe requerir confirmación explícita para eliminar[cite: 196].

---

 HU-02: Configuración del Tipo de Cambio de Referencia
Como Administrador del Sistema  
[cite_start]Quiero gestionar y activar registros de tipos de cambio del dólar estadounidense (USD) con respecto al colón costarricense (CRC) [cite: 23, 74]  
[cite_start]Para permitir la alternancia de visualización de montos sin alterar la base de datos oficial[cite: 23, 183].

Estimación: 2 PH  
 prioridad: Media  

 Criterios de Aceptación (Verificables):
1. [cite_start]Regla de Valor Positivo: El valor del tipo de cambio (CRC por USD) debe ser un número decimal estrictamente mayor a cero[cite: 129, 163, 164].
2. [cite_start]Exclusividad de Activo: Solo puede existir un (1) tipo de cambio marcado como "Activo" a la vez en el sistema para la operación ordinaria[cite: 188]. [cite_start]Al activar un registro, los demás deben desactivarse automáticamente[cite: 220].
3. [cite_start]Independencia de Red: La conversión y el tipo de cambio deben gestionarse de manera local; la aplicación debe funcionar correctamente sin necesidad de internet o llamadas a APIs externas[cite: 189].
4. [cite_start]Fecha de Vigencia: Cada registro de tipo de cambio debe contar con una fecha de vigencia explícita y mostrarse claramente en la interfaz[cite: 127, 187].