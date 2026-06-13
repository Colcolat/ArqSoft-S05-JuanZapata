# CitasApp - Arquitectura Hexagonal

**Alumno:** Josue Enmanuel Poot Mateo  
**Grupo:** 3B  
**Materia:** Arquitectura de Software  
**Institución:** Instituto Tecnológico de Software  

---

## Descripción

CitasApp es un sistema web de gestión de citas médicas desarrollado en C# bajo el paradigma de la **Arquitectura Hexagonal** (también conocida como patrón de Puertos y Adaptadores). Esta estructura permite administrar pacientes, médicos y citas asegurando que la lógica principal del negocio (el dominio) esté completamente aislada de los detalles de implementación tecnológica, como la interfaz de usuario (MVC) o la capa de persistencia de datos (archivos JSON).

---

## Funcionalidades

<img width="1440" height="811" alt="image" src="https://github.com/user-attachments/assets/e9e193ac-783b-4b49-86f5-b32146d99ec3" />
<img width="1440" height="814" alt="image" src="https://github.com/user-attachments/assets/e0ed0334-760f-4ba3-98c8-47cc18d1a3f6" />
<img width="1440" height="812" alt="image" src="https://github.com/user-attachments/assets/6eb5f109-ee55-4aea-979d-2af760dcf343" />
<img width="1440" height="811" alt="image" src="https://github.com/user-attachments/assets/75dc6735-9c64-4691-951e-b3cb72feb6e2" />
<img width="1440" height="803" alt="image" src="https://github.com/user-attachments/assets/f48cb352-96ff-4469-b2a0-8c70359c43fe" />

### Pacientes
* Listar todos los pacientes registrados en tarjetas visuales.
* Ver el detalle individual de cada paciente.
* Registrar nuevos pacientes.

### Médicos
* Listar médicos con su especialidad.
* Ver el detalle de cada médico.
* Registrar nuevos médicos.

### Citas
* Listar citas mostrando el nombre del paciente, médico asignado, fecha, hora y estado.
* Ver el detalle completo de cada cita.
* Registrar nuevas citas seleccionando paciente, médico, fecha, hora, motivo y estado.

---

## ¿Cómo funciona?

El sistema abandona el acoplamiento tradicional para seguir el patrón de **Arquitectura Hexagonal**, dividiéndose en capas que se comunican de adentro hacia afuera mediante contratos (interfaces):

1. **Capa de Dominio (El hexágono central):** Contiene las entidades puras del negocio (`Paciente`, `Medico`, `Cita`). Esta capa no sabe nada sobre la web, bases de datos o librerías externas.
2. **Capa de Aplicación (Puertos):** Define los casos de uso y los "Puertos" (interfaces). Aquí se establecen los contratos de entrada/salida (ej. `IPacienteRepository`), definiendo qué se necesita guardar o recuperar, pero no *cómo* se hace.
3. **Capa de Infraestructura (Adaptador de Salida):** Implementa los puertos definidos en la aplicación. Aquí se encuentra la lógica que lee, deserializa y sobreescribe los archivos JSON ubicados en `wwwroot/data/`. Si mañana se requiere usar SQL Server, solo se crea un nuevo adaptador sin modificar el centro del hexágono.
4. **Capa de Presentación (Adaptador de Entrada):** Utiliza ASP.NET Core MVC. Los **Controllers** reciben las peticiones HTTP del navegador, interactúan con la Capa de Aplicación enviando los datos, y finalmente devuelven las **Views** (HTML/Razor) al usuario.

---

## Tecnologías utilizadas

| Tecnología | Uso en la Arquitectura |
|---|---|
| **ASP.NET Core MVC (.NET 10)** | Adaptador de Entrada (Interfaz de Usuario / Web). |
| **C#** | Lenguaje de programación para todas las capas. |
| **Razor (.cshtml)** | Motor de plantillas para renderizar la vista al usuario. |
| **JSON** | Persistencia de datos empleada por el Adaptador de Salida. |
| **System.Text.Json** | Herramienta de la infraestructura para serialización de datos. |
| **Inyección de Dependencias** | Mecanismo nativo de .NET para conectar los puertos con los adaptadores. |
| **Bootstrap 5 & CSS propio** | Framework y estilos visuales de la aplicación. |
| **JetBrains Rider** | IDE de desarrollo. |

---

## Estructura del proyecto

El código refleja la separación por responsabilidades de la arquitectura hexagonal:

```text
CitasApp/
├── Core/                              # Centro del Hexágono
│   ├── Domain/
│   │   ├── Entities/
│   │   │   ├── Cita.cs
│   │   │   ├── Medico.cs
│   │   │   └── Paciente.cs
│   └── Application/
│       └── Ports/                     # Puertos de Salida
│           ├── ICitaRepository.cs
│           ├── IMedicoRepository.cs
│           └── IPacienteRepository.cs
├── Infrastructure/                    # Adaptadores de Salida
│   └── Persistence/
│       └── JsonAdapters/
│           ├── JsonCitaRepository.cs
│           ├── JsonMedicoRepository.cs
│           └── JsonPacienteRepository.cs
├── WebUI/                             # Adaptadores de Entrada
│   ├── Controllers/
│   │   ├── CitaController.cs
│   │   ├── MedicoController.cs
│   │   └── PacienteController.cs
│   ├── Views/
│   │   ├── Cita/...
│   │   ├── Medico/...
│   │   └── Paciente/...
│   └── wwwroot/
│       ├── css/
│       │   └── site.css
│       └── data/
│           ├── Pacientes.json
│           ├── Medicos.json
│           └── Citas.json


```

# Cláusula de uso de Inteligencia Artificial
Durante el desarrollo de este proyecto se utilizó inteligencia artificial (Claude - Anthropic) como herramienta de apoyo en las siguientes áreas:
Refactorización y Arquitectura: Apoyo para desacoplar el código y migrar la lógica hacia los principios de Puertos y Adaptadores, asegurando la correcta implementación de la Inyección de Dependencias.
* Depuración (debugging): Identificación y corrección de errores en tiempo de compilación y ejecución, específicamente en los adaptadores de infraestructura para la lectura/escritura de archivos JSON.
* Implementación de estilos y frontend: Diseño y escritura del CSS personalizado (site.css), incluyendo la creación de componentes visuales como tarjetas, avatares, cabeceras con gradiente, tablas estilizadas y diseño responsivo.
* El diseño lógico de los casos de uso, la estructura de las entidades del dominio y la integración general del backend fueron desarrollados por el alumno como parte del aprendizaje de la materia.

---

Proyecto desarrollado con fines académicos — 2026

