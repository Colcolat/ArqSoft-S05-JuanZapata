using System.Security.Claims;
using CitasApp.Application.DTOs;
using CitasApp.Application.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers;

[Authorize]
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
        if (User.IsInRole("Admin"))
            return View(_citaService.GetAll());

        var email = User.FindFirstValue(ClaimTypes.Email) ?? "";

        if (User.IsInRole("Medico"))
        {
            var medico = _medicoService.GetAll().FirstOrDefault(m => m.Email == email);
            return View(medico != null ? _citaService.ObtenerPorMedico(medico.Id) : new List<CitaViewModel>());
        }

        if (User.IsInRole("Paciente"))
        {
            var paciente = _pacienteService.GetAll().FirstOrDefault(p => p.Email == email);
            return View(paciente != null ? _citaService.ObtenerPorPaciente(paciente.Id) : new List<CitaViewModel>());
        }

        return View(new List<CitaViewModel>());
    }

    public IActionResult Detalle(int id)
    {
        var viewModel = _citaService.GetById(id);
        if (viewModel == null) return Content("Cita no encontrada");
        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Nuevo()
    {
        ViewBag.Pacientes = _pacienteService.GetAll();
        ViewBag.Medicos = _medicoService.GetAll();
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Nuevo(Cita cita)
    {
        _citaService.Add(cita);
        return RedirectToAction("Cita");
    }
}
