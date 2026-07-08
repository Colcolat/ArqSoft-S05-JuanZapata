# Documentación de Arquitectura (Modelo C4)

Esta documentación describe la arquitectura completa del proyecto **CitasApp** utilizando el Modelo C4, estructurada en 3 niveles de abstracción.

---

## 1. C4 Nivel 1 — Contexto

**Para quién es:** Para todo el equipo (incluyendo personas no técnicas, área de negocio y usuarios finales).  
**Qué pregunta responde:** ¿Cuál es el panorama general? ¿Quién usa el sistema y qué valor principal provee?

```mermaid
C4Context
    title Diagrama de Contexto de Sistema (Nivel 1) para CitasApp

    Person(paciente, "Paciente", "Persona que agenda y consulta sus citas médicas.")
    Person(medico, "Médico", "Profesional de la salud que revisa su agenda de citas.")
    Person(admin, "Administrador", "Personal de la clínica que gestiona el catálogo de médicos y pacientes.")

    System(citasApp, "CitasApp", "Plataforma de gestión de citas médicas que permite agendar, consultar y administrar citas.")

    Rel(paciente, citasApp, "Consulta sus citas", "Web Browser")
    Rel(medico, citasApp, "Consulta su agenda", "Web Browser")
    Rel(admin, citasApp, "Administra el sistema", "Web Browser")
```
