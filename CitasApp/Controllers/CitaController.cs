using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers;
using CitasApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class CitaController : Controller
{
    private readonly IWebHostEnvironment _env;
    public CitaController(IWebHostEnvironment env)
    {
        _env = env;  
    }
    public IActionResult Cita()
{
    var rutaCitas = Path.Combine(_env.WebRootPath, "data", "Citas.json");
    var rutaPacientes = Path.Combine(_env.WebRootPath, "data", "Pacientes.json");
    var rutaMedicos = Path.Combine(_env.WebRootPath, "data", "Medicos.json");

    var citas = JsonSerializer.Deserialize<List<Cita>>(System.IO.File.ReadAllText(rutaCitas),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    var pacientes = JsonSerializer.Deserialize<List<Paciente>>(System.IO.File.ReadAllText(rutaPacientes),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    var medicos = JsonSerializer.Deserialize<List<Medico>>(System.IO.File.ReadAllText(rutaMedicos),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    var viewModel = citas.Select(c => new CitaViewModel
    {
        Id = c.Id,
        NombrePaciente = pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Nombre + " " +
                         pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Apellido,
        NombreMedico = medicos.FirstOrDefault(m => m.Id == c.MedicoId)?.Nombre,
        Fecha = c.Fecha,
        FechaHora = c.FechaHora,
        Motivo = c.Motivo,
        Estado = c.Estado
    }).ToList();

    return View(viewModel);
}

public IActionResult Detalle(int id)
{
    var rutaCitas = Path.Combine(_env.WebRootPath, "data", "Citas.json");
    var rutaPacientes = Path.Combine(_env.WebRootPath, "data", "Pacientes.json");
    var rutaMedicos = Path.Combine(_env.WebRootPath, "data", "Medicos.json");

    var citas = JsonSerializer.Deserialize<List<Cita>>(System.IO.File.ReadAllText(rutaCitas),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    var pacientes = JsonSerializer.Deserialize<List<Paciente>>(System.IO.File.ReadAllText(rutaPacientes),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    var medicos = JsonSerializer.Deserialize<List<Medico>>(System.IO.File.ReadAllText(rutaMedicos),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    var cita = citas.FirstOrDefault(c => c.Id == id);
    if (cita == null) return Content("Cita no encontrada");

    var paciente = pacientes.FirstOrDefault(p => p.Id == cita.PacienteId);
    var medico = medicos.FirstOrDefault(m => m.Id == cita.MedicoId);

    var viewModel = new CitaViewModel
    {
        Id = cita.Id,
        NombrePaciente = paciente?.Nombre + " " + paciente?.Apellido,
        NombreMedico = medico?.Nombre,
        Fecha = cita.Fecha,
        FechaHora = cita.FechaHora,
        Motivo = cita.Motivo,
        Estado = cita.Estado
    };

    return View(viewModel);
}

public IActionResult Nuevo()
{
    var rutaPacientes = Path.Combine(_env.WebRootPath, "data", "Pacientes.json");
    var rutaMedicos   = Path.Combine(_env.WebRootPath, "data", "Medicos.json");

    ViewBag.Pacientes = JsonSerializer.Deserialize<List<Paciente>>(System.IO.File.ReadAllText(rutaPacientes),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    ViewBag.Medicos = JsonSerializer.Deserialize<List<Medico>>(System.IO.File.ReadAllText(rutaMedicos),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return View();
}

[HttpPost]
public IActionResult Nuevo(Cita cita)
{
    var ruta = Path.Combine(_env.WebRootPath, "data", "Citas.json"); // ← corregido

    var json = System.IO.File.ReadAllText(ruta);
    var citas = JsonSerializer.Deserialize<List<Cita>>(json,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    cita.Id = citas.Max(p => p.Id) + 1;
    citas.Add(cita);

    var nuevoJson = JsonSerializer.Serialize(citas, new JsonSerializerOptions { WriteIndented = true });
    System.IO.File.WriteAllText(ruta, nuevoJson);
    return RedirectToAction("Cita");
}
}