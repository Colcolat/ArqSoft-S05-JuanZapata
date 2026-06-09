using CitasApp.Application.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers;

public class CitaController : Controller
{
    private readonly ICitaService _citaService;
    private readonly IPacienteService _pacienteService;
    private readonly IMedicoService _medicoService;

    public CitaController(ICitaService citaService, IPacienteService pacienteService, IMedicoService medicoService)
    {
        _citaService = citaService;
        _pacienteService = pacienteService;
        _medicoService = medicoService;
    }

    public IActionResult Cita()
    {
        var viewModel = _citaService.GetAll();
        return View(viewModel);
    }

    public IActionResult Detalle(int id)
    {
        var viewModel = _citaService.GetById(id);
        if (viewModel == null) return Content("Cita no encontrada");
        return View(viewModel);
    }

    public IActionResult Nuevo()
    {
        ViewBag.Pacientes = _pacienteService.GetAll();
        ViewBag.Medicos = _medicoService.GetAll();
        return View();
    }

    [HttpPost]
    public IActionResult Nuevo(Cita cita)
    {
        _citaService.Add(cita);
        return RedirectToAction("Cita");
    }
}
