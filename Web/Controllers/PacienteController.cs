using CitasApp.Application.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers;

public class PacienteController : Controller
{
    private readonly IPacienteService _pacienteService;

    public PacienteController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    public IActionResult Paciente()
    {
        var pacientes = _pacienteService.GetAll();
        return View(pacientes);
    }

    public IActionResult Detalle(int id)
    {
        var paciente = _pacienteService.GetById(id);
        if (paciente == null) return Content("Paciente no encontrado");
        return View(paciente);
    }

    public IActionResult Nuevo()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Nuevo(Paciente paciente)
    {
        _pacienteService.Add(paciente);
        return RedirectToAction("Paciente");
    }
}
