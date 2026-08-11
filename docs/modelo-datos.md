

Crea o reemplaza el contenido de este archivo en `docs/modelo-datos.md`:

```markdown
# Modelo de Datos — Gestión Licitaciones XP

**Curso:** ITI-822 Metodologías Ágiles  
**Proyecto Final:** Extreme Programming (XP)  
**Motor de Base de Datos:** PostgreSQL 16 (Alpine)  
**ORM:** Entity Framework Core 9  

---

## 1. Descripción del Dominio de Datos

El esquema de datos está estructurado para soportar el ciclo de vida completo de un proceso licitatorio: desde el registro de usuarios y la publicación de bases de licitación, hasta la recepción de ofertas y la adjudicación final.

---

## 2. Diagrama Entidad-Relación (Mermaid ER)

```mermaid
erDiagram
    USUARIO {
        uuid id PK
        string nombre
        string email
        string password_hash
        string rol
        boolean activo
        datetime fecha_creacion
    }

    LICITACION {
        uuid id PK
        string codigo UK
        string titulo
        string descripcion
        decimal presupuesto_estimado
        string estado
        datetime fecha_apertura
        datetime fecha_cierre
        uuid usuario_creador_id FK
    }

    OFERTA {
        uuid id PK
        decimal monto_propuesto
        string propuesta_tecnica
        string estado
        datetime fecha_presentacion
        uuid licitacion_id FK
        uuid proveedor_id FK
    }

    EVALUACION {
        uuid id PK
        decimal puntaje_tecnico
        decimal puntaje_economico
        decimal puntaje_total
        string observaciones
        datetime fecha_evaluacion
        uuid oferta_id FK
        uuid evaluador_id FK
    }

    ADJUDICACION {
        uuid id PK
        datetime fecha_adjudicacion
        string numero_resolucion
        string observaciones
        uuid licitacion_id FK
        uuid oferta_ganadora_id FK
    }

    USUARIO ||--o{ LICITACION : "crea/publica}"
    USUARIO ||--o{ OFERTA : "presenta (como proveedor)}
    USUARIO ||--o{ EVALUACION : "evalua (como evaluador)}
    LICITACION ||--o{ OFERTA : "recibe"}
    OFERTA ||--o| EVALUACION : "tiene"
    LICITACION ||--o| ADJUDICACION : "culmina en"
    OFERTA ||--o| ADJUDICACION : "es seleccionada en"


   