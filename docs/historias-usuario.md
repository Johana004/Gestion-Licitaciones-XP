 Catálogo de Historias de Usuario (XP)

Este documento detalla las Historias de Usuario que guían el diseño, desarrollo y pruebas del sistema.


 ITERACIÓN 1: El Núcleo del Dominio y Configuración Base

 HU-01: Registro de Proveedores Unificados
Como Encargado de Licitaciones  
Quiero registrar proveedores en el sistema mediante su nombre  
Para poder asociarlos posteriormente a las ofertas de las licitaciones

 Estimación:3 Puntos de Historia (PH)  
 Prioridad: Alta  

Criterios de Aceptación (Verificables):
1. Validación de Caracteres: El nombre del proveedor solo puede contener letras, números, espacios, puntos, comas y paréntesis normales. Cualquier otro carácter especial debe ser rechazado con un error descriptivo
2. Normalización Estricta: Antes de persistir o validar duplicidad, el nombre debe eliminar espacios a los lados, reducir espacios intermedios repetidos, normalizar Unicode y compararse sin distinguir mayúsculas ni minúsculas (Ej: "Empresa Central" es idéntica a "  empresa   central ")
3.Unicidad en PostgreSQL: El sistema debe impedir el registro de dos proveedores con el mismo nombre normalizado mediante una validación del lado del servidor y un índice único a nivel de base de datos
4. Borrado Lógico/Físico Seguro: Un proveedor no puede ser eliminado físicamente si ya cuenta con ofertas registradas. Se debe requerir confirmación explícita para eliminar


 HU-02: Configuración del Tipo de Cambio de Referencia
Como Administrador del Sistema  
Quiero gestionar y activar registros de tipos de cambio del dólar estadounidense (USD) con respecto al colón costarricense (CRC) 
Para permitir la alternancia de visualización de montos sin alterar la base de datos oficial

Estimación: 2 PH  
 prioridad: Media  

 Criterios de Aceptación (Verificables):
1. Regla de Valor Positivo: El valor del tipo de cambio (CRC por USD) debe ser un número decimal estrictamente mayor a cero
2. Exclusividad de Activo: Solo puede existir un (1) tipo de cambio marcado como "Activo" a la vez en el sistema para la operación ordinaria. Al activar un registro, los demás deben desactivarse automáticamente
3. Independencia de Red: La conversión y el tipo de cambio deben gestionarse de manera local; la aplicación debe funcionar correctamente sin necesidad de internet o llamadas a APIs externas
4. Fecha de Vigencia: Cada registro de tipo de cambio debe contar con una fecha de vigencia explícita y mostrarse claramente en la interfaz