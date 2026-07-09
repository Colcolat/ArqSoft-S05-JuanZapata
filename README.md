# CitasApp

**Alumno:** Josue Enmanuel Poot Mateo  
**Grupo:** 3B  
**Materia:** Arquitectura de Software  
**Institución:** Instituto Tecnológico de Software  

---

## Diagramas de Arquitectura

Los diagramas C4 (Contexto, Contenedores, Componentes) y el flujo general del sistema están disponibles en:

**[docs/diagramas.md](docs/diagramas.md)**

---

## Descripción

CitasApp es un sistema web de gestión de citas médicas desarrollado con ASP.NET Core MVC. Permite administrar pacientes, médicos y citas de forma sencilla. Esta versión del proyecto ha sido refactorizada hacia una **Arquitectura Hexagonal (Puertos y Adaptadores)**, lo que permite una clara separación de responsabilidades aislando la lógica de negocio (dominio) de los detalles técnicos como la interfaz de usuario y la persistencia de datos.

---

## Funcionalidades

<img width="1440" height="811" alt="image" src="https://github.com/user-attachments/assets/e9e193ac-783b-4b49-86f5-b32146d99ec3" />
<img width="1440" height="814" alt="image" src="https://github.com/user-attachments/assets/e0ed0334-760f-4ba3-98c8-47cc18d1a3f6" />
<img width="1440" height="812" alt="image" src="https://github.com/user-attachments/assets/6eb5f109-ee55-4aea-979d-2af760dcf343" />
<img width="1440" height="811" alt="image" src="https://github.com/user-attachments/assets/75dc6735-9c64-4691-951e-b3cb72feb6e2" />
<img width="1440" height="803" alt="image" src="https://github.com/user-attachments/assets/f48cb352-96ff-4469-b2a0-8c70359c43fe" />

### Pacientes
- Listar todos los pacientes registrados en tarjetas visuales.
- Ver el detalle individual de cada paciente.
- Registrar nuevos pacientes.

### Médicos
- Listar médicos con su especialidad.
- Ver el detalle de cada médico.
- Registrar nuevos médicos.

### Citas
- Listar citas mostrando el nombre del paciente, médico asignado, fecha, hora y estado.
- Ver el detalle completo de cada cita.
- Registrar nuevas citas seleccionando paciente, médico, fecha, hora, motivo y estado.

---

## ¿Cómo funciona? (Arquitectura Hexagonal)

El sistema evoluciona del tradicional patrón MVC para implementar el patrón de **Puertos y Adaptadores**, dividiéndose en las siguientes capas lógicas:

1. **Dominio (Core):** Contiene las entidades principales (`Paciente`, `Medico`, `Cita`) y las reglas de negocio puras. No tiene dependencias externas.
2. **Aplicación (Puertos):** Define los casos de uso (servicios) y las interfaces de los repositorios. Dicta *qué* debe hacer el sistema sin importar *cómo* se implemente.
3. **Infraestructura (Adaptadores de Salida):** Implementa las interfaces de la capa de aplicación. Aquí se encuentra el acceso a datos (mediante lectura de archivos JSON o bases de datos con Entity Framework Core/SQLite).
4. **Presentación (Adaptadores de Entrada):** La capa web en ASP.NET Core MVC. Los controladores reciben las peticiones HTTP (ej. `/Paciente/Paciente`), invocan los servicios de la capa de aplicación y devuelven las vistas Razor (`.cshtml`).

---

## Tecnologías utilizadas

| Tecnología | Uso |
|---|---|
| **ASP.NET Core MVC (.NET 10)** | Framework principal para la capa de presentación |
| **C#** | Lenguaje de programación |
| **Entity Framework Core / SQLite** | ORM y base de datos para la configuración de Identity e Infraestructura |
| **JSON / System.Text.Json** | Serialización y deserialización para persistencia de datos (Adaptadores) |
| **Razor (.cshtml)** | Motor de plantillas para las vistas |
| **Bootstrap 5** | Framework CSS base |
| **CSS personalizado** | Estilos visuales propios de la aplicación |
| **JetBrains Rider** | IDE de desarrollo |

---

## Estructura del proyecto

Bajo el enfoque hexagonal, la organización lógica del proyecto se divide de la siguiente manera para proteger el núcleo del negocio:
<pre>
CitasApp/
├── Core/                       # Capa de Dominio y Aplicación (El Hexágono)
│   ├── Entities/               # Modelos de dominio (Cita, Medico, Paciente)
│   ├── Interfaces/             # Puertos (IRepositories, IServices)
│   └── Services/               # Lógica de negocio y casos de uso
├── Infrastructure/             # Capa de Infraestructura (Adaptadores de salida)
│   ├── Data/                   # DbContext (EF Core) y configuraciones
│   └── Repositories/           # Implementación de persistencia (JSON/SQLite)
├── Web/                        # Capa de Presentación (Adaptadores de entrada)
│   ├── Controllers/            # Controladores MVC (CitaController, etc.)
│   ├── Views/                  # Vistas Razor por cada entidad
│   └── wwwroot/                # Archivos estáticos (CSS, JS, imágenes, datos JSON)
└── Program.cs                  # Configuración de Inyección de Dependencias (IoC)
</pre>

## Cláusula de uso de Inteligencia Artificial

Durante el desarrollo de este proyecto se utilizó **inteligencia artificial (Claude - Anthropic)** como herramienta de apoyo en las siguientes áreas:

- **Depuración (debugging):** Identificación y corrección de errores en tiempo de compilación y ejecución, incluyendo errores de deserialización JSON, referencias nulas y problemas de enrutamiento MVC.
- **Implementación de estilos y frontend:** Diseño y escritura del CSS personalizado (`site.css`), incluyendo la creación de componentes visuales como tarjetas, avatares, cabeceras con gradiente, tablas estilizadas y diseño responsivo.

El diseño lógico de la arquitectura, la estructura del proyecto y la implementación del backend fueron desarrollados por el alumno como parte del aprendizaje de la materia.

---

*Proyecto desarrollado con fines académicos — 2026*






