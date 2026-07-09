# Diagramas de Arquitectura — CitasApp

**Alumno:** Josue Enmanuel Poot Mateo  
**Materia:** Arquitectura de Software  

---

## Flujo General

Visión simplificada del recorrido de una petición desde el cliente hasta la capa de persistencia, pasando por los patrones GoF implementados.

```mermaid
graph TD
    A[Cliente] -->|HTTP| B[CitasApp.Api]
    B --> C[Application]
    C --> D[RepositoryFactory]
    C --> E[LoggingPacienteRepository - Decorator]
    C --> F[(Persistencia JSON)]
```

---

## C4 Nivel 1 — Contexto

Quién usa el sistema y para qué. No aparecen tecnologías ni detalles internos.

```mermaid
graph TD
    P[Paciente] -->|agenda citas| CA[CitasApp\nSistema de gestión de citas médicas]
    M[Médico] -->|revisa su agenda| CA
```

---

## C4 Nivel 2 — Contenedores

Las piezas técnicas grandes: la API, cómo se comunica y dónde persiste.

```mermaid
graph LR
    CL["Cliente\n(Postman / Frontend)"] -->|HTTP| API["CitasApp.Api\n(ASP.NET Core)"]
    API -->|JSON| CL
    API --> DB[("Persistencia\n(JSON / futuro RDS)")]
```

---

## C4 Nivel 3 — Componentes dentro de CitasApp.Api

Descomposición interna: controllers, capa de aplicación y patrones GoF.

```mermaid
graph TD
    subgraph CitasApp.Api
        CTRL["Controllers\n/api/pacientes\n/api/medicos\n/api/citas"] --> APP["Application\nPacienteService · MedicoService · CitaService"]
        APP --> RF["RepositoryFactory\n(creacional — Factory)"]
        APP --> LOG["LoggingPacienteRepository\n(estructural — Decorator)"]
        RF --> REPO[("Repositorios\nJsonPacienteRepository\nMemoriaPacienteRepository")]
        LOG --> REPO
    end
```
