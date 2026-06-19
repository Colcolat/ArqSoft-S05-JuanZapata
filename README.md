# CitasApp

Sistema de gestión de citas médicas desarrollado como proyecto de la materia **Arquitectura de Software**. Implementa **Arquitectura Hexagonal (Ports & Adapters)**, separando el dominio del negocio de los detalles de infraestructura (persistencia) y de los puntos de entrada (Web MVC y API REST).

## Descripción

CitasApp administra tres entidades principales: **Pacientes**, **Médicos** y **Citas**. El sistema permite:

- Consultar el listado y detalle de pacientes y médicos.
- Programar y confirmar citas médicas.
- Consultar citas filtradas por paciente.
- Exponer toda la información anterior también como API REST, además de una **API de Calculadora** (suma, resta, multiplicación, división) usada como ejercicio independiente de diseño de endpoints.

El punto central del diseño es que la lógica de negocio (capa `Domain`) nunca depende de cómo se guardan los datos ni de cómo se exponen. Los **Ports** son interfaces (`IPacienteRepository`, `IMedicoRepository`, `ICitaRepository`) y los **Adapters** son sus implementaciones concretas, intercambiables sin tocar los controladores.

## Arquitectura

```
CitasApp/
├── Domain/            → Entidades (Paciente, Medico, Cita) e interfaces (Ports)
├── Infrastructure/     → Adapters: repositorios en JSON y en CSV
├── Web/                → Frontend MVC (Razor Views) + Controllers
└── Api/                → API REST (Controllers) + API de Calculadora
```

**Flujo de dependencias:** `Web` y `Api` dependen de `Domain` e `Infrastructure`, pero `Domain` no depende de nada — es el núcleo aislado de la arquitectura hexagonal.

El proyecto soporta tres adapters de persistencia intercambiables mediante inyección de dependencias en `Program.cs` (JSON, CSV, y un bloque preparado para SQLite), sin que el dominio ni los controladores cambien una sola línea.

## Tecnologías usadas

| Categoría | Tecnología |
|---|---|
| Lenguaje | C# |
| Framework | ASP.NET Core (.NET 10) |
| Patrón de arquitectura | Hexagonal / Ports & Adapters |
| Frontend Web | ASP.NET MVC + Razor Views (`.cshtml`) |
| Estilos | CSS |
| API | ASP.NET Core Web API (Controllers REST) |
| Persistencia | Adapters intercambiables: JSON (`System.Text.Json`) y CSV |
| Inyección de dependencias | `Microsoft.Extensions.DependencyInjection` (built-in de ASP.NET Core) |
| Cliente de prueba | HTML + JavaScript (fetch API) |
| Control de versiones | Git, Conventional Commits |

## Proyectos de la solución

- **Domain** — Modelos (`Paciente`, `Medico`, `Cita`) e interfaces de repositorio. Sin dependencias externas.
- **Infrastructure** — Implementaciones de los repositorios:
  - `Json*Repository` — persistencia en archivos `.json`.
  - `Csv*Repository` — persistencia en archivos `.csv`.
- **Web** — Aplicación MVC con vistas Razor para pacientes, médicos y citas.
- **Api** — Controladores REST (`PacientesController`, `MedicosController`, `CitasController`) y `CalculadoraController`.

## Endpoints de la API

### Recursos del dominio

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/pacientes` | Lista todos los pacientes |
| GET | `/api/pacientes/{id}` | Obtiene un paciente por ID |
| GET | `/api/medicos` | Lista todos los médicos |
| GET | `/api/medicos/{id}` | Obtiene un médico por ID |
| GET | `/api/citas` | Lista todas las citas |
| GET | `/api/citas/porpaciente/{pacienteId}` | Lista las citas de un paciente |

### Calculadora

| Método | Ruta | Parámetros |
|---|---|---|
| GET | `/api/calculadora/sumar` | `a`, `b` |
| GET | `/api/calculadora/restar` | `a`, `b` |
| GET | `/api/calculadora/multiplicar` | `a`, `b` |
| GET | `/api/calculadora/dividir` | `a`, `b` (valida división entre cero) |

## Cómo ejecutar

```bash
# Aplicación Web (MVC)
dotnet run --project Web

# API REST
dotnet run --project Api
```

La API corre por defecto en `http://localhost:5044`. Incluye CORS habilitado para permitir pruebas desde clientes HTML externos.

Para cambiar el adapter de persistencia de `Web` (JSON / CSV / SQLite), edita los bloques comentados en `Web/Program.cs` — las interfaces (`Ports`) no cambian.

## Autor

Juan Zapata — TSU Ingeniería en Software, Instituto Tecnológico de Software.
Este README y el HTML fue hecho con la ayuda de Codex
