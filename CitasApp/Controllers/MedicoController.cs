using CitasApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class MedicoController :  Controller
{
    private readonly IWebHostEnvironment _env;
    public MedicoController(IWebHostEnvironment env)
    {
        _env = env;  
    }
    public IActionResult Medico()
    {
            var ruta = Path.Combine(_env.WebRootPath, "data", "Medicos.json");

            if (!System.IO.File.Exists(ruta))
                return Content($"Archivo no encontrado en: {ruta}");

            var json = System.IO.File.ReadAllText(ruta);
            var medicos = JsonSerializer.Deserialize<List<Medico>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (medicos == null)
                return Content("El JSON no se pudo deserializar");

            return View(medicos);
    }
    
    public IActionResult Detalle(int id)
    {
        var ruta = Path.Combine(_env.WebRootPath, "data", "Medicos.json");
        var json = System.IO.File.ReadAllText(ruta);
        var medicos = JsonSerializer.Deserialize<List<Medico>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var medico = medicos.FirstOrDefault(p => p.Id == id);

        if (medico == null)
            return Content("Medico no encontrado");

        return View(medico);
    }
    
    public IActionResult Nuevo()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Nuevo(Medico medico)
    {
        var ruta = Path.Combine(_env.WebRootPath, "data", "Medicos.json");
        
        var json = System.IO.File.ReadAllText(ruta);
        var medicos = JsonSerializer.Deserialize<List<Medico>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        medico.Id = medicos.Max(p => p.Id) + 1;
        medicos.Add(medico);
        
        var nuevoJson = JsonSerializer.Serialize(medicos, new JsonSerializerOptions {WriteIndented =  true});
        System.IO.File.WriteAllText(ruta, nuevoJson);
        return RedirectToAction("Medico");
    }
}