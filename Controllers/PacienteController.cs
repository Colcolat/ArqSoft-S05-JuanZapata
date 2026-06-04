using CitasApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CitasApp.Controllers;

public class PacienteController : Controller
{
    private readonly IWebHostEnvironment _env;

    public PacienteController(IWebHostEnvironment env)
    {
        _env = env;  
    }

    public IActionResult Paciente()
    {
        var ruta = Path.Combine(_env.WebRootPath, "data", "Pacientes.json");

        if (!System.IO.File.Exists(ruta))
            return Content($"Archivo no encontrado en: {ruta}");

        var json = System.IO.File.ReadAllText(ruta);
        var pacientes = JsonSerializer.Deserialize<List<Paciente>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (pacientes == null)
            return Content("El JSON no se pudo deserializar");

        return View(pacientes);
    }

    public IActionResult Detalle(int id)
    {
        return View();
    }

    public IActionResult Nuevo()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Nuevo(Paciente paciente)
    {
        var ruta = Path.Combine(_env.WebRootPath, "data", "Pacientes.json");
        
        var json = System.IO.File.ReadAllText(ruta);
        var pacientes = JsonSerializer.Deserialize<List<Paciente>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        paciente.Id = pacientes.Max(p => p.Id) + 1;
        pacientes.Add(paciente);
        
        var nuevoJson = JsonSerializer.Serialize(pacientes, new JsonSerializerOptions {WriteIndented =  true});
        System.IO.File.WriteAllText(ruta, nuevoJson);
        return RedirectToAction("Paciente");
    }
}   