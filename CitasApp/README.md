# CitasApp

**Alumno:** Josue Enmanuel Poot Mateo  
**Grupo:** 3B  
**Materia:** Arquitectura de Software  
**Institución:** Instituto Tecnológico de Software  

---

## Descripción

CitasApp es un sistema web de gestión de citas médicas desarrollado con ASP.NET Core MVC. Permite administrar pacientes, médicos y citas de forma sencilla, sin necesidad de una base de datos, utilizando archivos JSON como capa de persistencia de datos.

---

## Funcionalidades

<img width="1440" height="811" alt="image" src="https://github.com/user-attachments/assets/e9e193ac-783b-4b49-86f5-b32146d99ec3" />
<img width="1440" height="814" alt="image" src="https://github.com/user-attachments/assets/e0ed0334-760f-4ba3-98c8-47cc18d1a3f6" />
<img width="1440" height="812" alt="image" src="https://github.com/user-attachments/assets/6eb5f109-ee55-4aea-979d-2af760dcf343" />
<img width="1440" height="811" alt="image" src="https://github.com/user-attachments/assets/75dc6735-9c64-4691-951e-b3cb72feb6e2" />
<img width="1440" height="803" alt="image" src="https://github.com/user-attachments/assets/f48cb352-96ff-4469-b2a0-8c70359c43fe" />


### Pacientes
- Listar todos los pacientes registrados en tarjetas visuales
- Ver el detalle individual de cada paciente
- Registrar nuevos pacientes

### Médicos
- Listar médicos con su especialidad
- Ver el detalle de cada médico
- Registrar nuevos médicos

### Citas
- Listar citas mostrando el nombre del paciente, médico asignado, fecha, hora y estado
- Ver el detalle completo de cada cita
- Registrar nuevas citas seleccionando paciente, médico, fecha, hora, motivo y estado

---

## ¿Cómo funciona?

El sistema sigue el patrón **MVC (Modelo - Vista - Controlador)**:

1. El usuario accede a una ruta desde el navegador (ej. `/Paciente/Paciente`)
2. El **Controller** correspondiente recibe la solicitud, lee el archivo JSON desde `wwwroot/data/`
3. Deserializa el JSON en una lista de objetos del **Model**
4. Envía los datos a la **View** (archivo `.cshtml`) que los renderiza en HTML

Para agregar registros, el formulario envía un `POST` al controller, que agrega el nuevo objeto a la lista y sobreescribe el archivo JSON con los datos actualizados.

---

## Tecnologías utilizadas

| Tecnología | Uso |
|---|---|
| **ASP.NET Core MVC (.NET 10)** | Framework principal del backend |
| **C#** | Lenguaje de programación |
| **Razor (.cshtml)** | Motor de plantillas para las vistas |
| **JSON** | Persistencia de datos (sin base de datos) |
| **System.Text.Json** | Serialización y deserialización de datos |
| **Bootstrap 5** | Framework CSS base |
| **CSS personalizado** | Estilos visuales propios de la aplicación |
| **JetBrains Rider** | IDE de desarrollo |

---

## Estructura del proyecto

```
CitasApp/
├── Controllers/
│   ├── CitaController.cs
│   ├── MedicoController.cs
│   └── PacienteController.cs
├── Models/
│   ├── Cita.cs            # Incluye CitaViewModel
│   ├── Medico.cs
│   └── Paciente.cs
├── Views/
│   ├── Cita/
│   │   ├── Cita.cshtml
│   │   ├── Detalle.cshtml
│   │   └── Nuevo.cshtml
│   ├── Medico/
│   │   ├── Medico.cshtml
│   │   ├── Detalle.cshtml
│   │   └── Nuevo.cshtml
│   ├── Paciente/
│   │   ├── Paciente.cshtml
│   │   ├── Detalle.cshtml
│   │   └── Nuevo.cshtml
│   ├── Home/
│   │   └── Index.cshtml
│   └── Shared/
│       └── _Layout.cshtml
└── wwwroot/
    ├── css/
    │   └── site.css
    └── data/
        ├── Pacientes.json
        ├── Medicos.json
        └── Citas.json
```

---



## Cláusula de uso de Inteligencia Artificial

Durante el desarrollo de este proyecto se utilizó **inteligencia artificial (Claude - Anthropic)** como herramienta de apoyo en las siguientes áreas:

- **Depuración (debugging):** Identificación y corrección de errores en tiempo de compilación y ejecución, incluyendo errores de deserialización JSON, referencias nulas y problemas de enrutamiento MVC.
- **Implementación de estilos y frontend:** Diseño y escritura del CSS personalizado (`site.css`), incluyendo la creación de componentes visuales como tarjetas, avatares, cabeceras con gradiente, tablas estilizadas y diseño responsivo.

El diseño lógico de la arquitectura, la estructura del proyecto y la implementación del backend fueron desarrollados por el alumno como parte del aprendizaje de la materia.

---

*Proyecto desarrollado con fines académicos — 2026*
