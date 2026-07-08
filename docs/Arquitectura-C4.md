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

---

## 2. C4 Nivel 2 — Contenedores

**Para quién es:** Para el equipo técnico, arquitectos de software y desarrolladores.  
**Qué pregunta responde:** ¿Cuáles son las grandes piezas técnicas que conforman el sistema, de qué tecnologías están hechas y cómo se comunican entre sí?

```mermaid
C4Container
    title Diagrama de Contenedores (Nivel 2) para CitasApp

    Person(usuario, "Usuarios", "Pacientes, Médicos y Administradores.")

    System_Boundary(citasApp_bound, "CitasApp") {
        Container(webApp, "Aplicación Web (MVC)", "ASP.NET Core MVC", "Provee la interfaz de usuario renderizando vistas HTML y consumiendo los servicios.")
        Container(api, "API REST", "ASP.NET Core Web API", "Expone endpoints para integraciones y clientes externos.")
        ContainerDb(database, "Persistencia de Datos", "JSON / SQLite", "Almacena los registros de pacientes, médicos y citas médicas.")
    }

    Rel(usuario, webApp, "Visita y usa", "HTTPS")
    Rel(usuario, api, "Consume datos vía", "JSON/HTTPS")
    Rel(webApp, database, "Lee y escribe en", "File I/O / SQL")
    Rel(api, database, "Lee y escribe en", "File I/O / SQL")
```

---

## 3. C4 Nivel 3 — Componentes

**Para quién es:** Para los desarrolladores de software del equipo.  
**Qué pregunta responde:** ¿Cómo está estructurado internamente el contenedor principal? ¿Cuáles son los módulos, servicios, repositorios y patrones de diseño utilizados?

```mermaid
C4Component
    title Diagrama de Componentes (Nivel 3) para la Aplicación Web

    Container_Boundary(webApp_bound, "Aplicación Web MVC / API") {
        Component(controllers, "Controladores MVC / API", "ASP.NET Controllers", "Manejan las peticiones HTTP y renderizan vistas. (Ej. CitasController, ApiCitasController)")
        
        Boundary(app_layer, "CitasApp.Application (Capa de Aplicación)") {
            Component(services, "Servicios (Application Services)", "C# Classes", "Contienen la lógica de orquestación. (Ej. CitaService, PacienteService, MedicoService)")
        }
        
        Boundary(domain_layer, "CitasApp.Domain (Capa de Dominio)") {
            Component(models, "Entidades del Dominio", "C# Classes", "Clases base del negocio. (Cita, Paciente, Medico)")
            Component(interfaces, "Interfaces de Repositorios", "C# Interfaces", "Contratos para la persistencia. (ICitaRepository)")
        }
        
        Boundary(infra_layer, "CitasApp.Infrastructure (Capa de Infraestructura)") {
            Component(repos, "Repositorios Concretos", "Patrón Factory / Repository", "Implementan el acceso a datos. (Ej. JsonCitaRepository, SqliteCitaRepository)")
            Component(observers, "Notificadores", "Patrón Observer", "Notifican eventos. (Ej. EmailObserver, SmsObserver)")
        }
    }
    
    ContainerDb(database, "Persistencia de Datos", "JSON / SQLite", "Archivos JSON y base de datos relacional.")

    Rel(controllers, services, "Delega lógica a", "Inyección de Dependencias")
    Rel(services, interfaces, "Usa contratos de", "Interfaces")
    Rel(services, models, "Usa y retorna", "Entidades")
    Rel(repos, interfaces, "Implementa", "Herencia")
    Rel(services, observers, "Notifica a", "Patrón Observer")
    Rel(repos, database, "Lee / Escribe", "I/O")
```
