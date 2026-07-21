# ADR — Suite de Pruebas Unitarias CitasApp

**Fecha:** 2026-07-21  
**Estado:** Activo  
**Rama:** CI/CD

## Contexto

Tras la refactorización en la rama `CodeSmells` (extracción de responsabilidades del `CitaController`) era necesario garantizar que el comportamiento del dominio se mantiene correcto ante futuros cambios, sin depender de pruebas manuales.

## Decisión

Se agregó un proyecto `CitasApp.Tests` usando **xUnit** con el patrón **Arrange-Act-Assert**, cubriendo tres clases del dominio:

| Clase probada | Razón de elección |
|---|---|
| `CitaFactory` | Es el punto de creación de citas; debe garantizar siempre `Estado = "Pendiente"` |
| `CitaValidator` | Centraliza las reglas de negocio de validación; un error aquí afecta toda la app |
| `Cita` (modelo) | Tiene data annotations que deben funcionar correctamente para la validación de formularios |

## Pruebas incluidas

**CitaFactoryTests (3 pruebas)**
- `Construir_ConDatosValidos_CreaCitaConEstadoPendiente` — Estado siempre "Pendiente"
- `Construir_ConDatosValidos_AsignaPacienteYMedicoCorrectos` — IDs asignados correctamente
- `Construir_ConDatosValidos_GuardaMotivoCorrectamente` — Motivo sin modificaciones

**CitaValidatorTests (4 pruebas)**
- `EsValida_ConCitaCompleta_RetornaTrue`
- `EsValida_SinMedico_RetornaFalse`
- `EsValida_SinMotivo_RetornaFalse`
- `ObtenerErrores_CitaVacia_RetornaCuatroErrores`

**CitaModelTests (3 pruebas)**
- `Cita_ConTodosLosCampos_NoTieneErroresDeValidacion`
- `Cita_SinFecha_TieneErrorDeValidacion`
- `Cita_ConPacienteIdCero_TieneErrorDeRango`

## Pipeline CI

Se configuró `.github/workflows/ci.yml` con GitHub Actions:  
`push` → restore → build → `dotnet test`

Cada push dispara el pipeline automáticamente. Un check verde confirma que las 10 pruebas pasan; un fallo aparece en rojo con el log exacto del error.

## Consecuencias

- Cualquier cambio que rompa las reglas del dominio es detectado antes del merge.
- El pipeline corre en `ubuntu-latest` con .NET 10.
