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

    [Authorize(Roles = "Admin,Paciente")]
    public IActionResult Nuevo()
    {
        ViewBag.Medicos = _medicoService.GetAll();

        if (User.IsInRole("Paciente"))
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? "";
            var paciente = _pacienteService.GetAll().FirstOrDefault(p => p.Email == email);
            ViewBag.PacienteFijo = paciente;
        }
        else
        {
            ViewBag.Pacientes = _pacienteService.GetAll();
        }

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Paciente")]
    public IActionResult Nuevo(Cita cita, string hora, string minuto, string ampm)
    {
        cita.FechaHora = $"{hora}:{minuto} {ampm}";

        if (User.IsInRole("Paciente"))
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? "";
            var paciente = _pacienteService.GetAll().FirstOrDefault(p => p.Email == email);
            if (paciente != null) cita.PacienteId = paciente.Id;
            cita.Estado = "Pendiente";
        }

        _citaService.Add(cita);
        return RedirectToAction("Cita");
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Medico")]
    public IActionResult CambiarEstado(int id, string estado)
    {
        _citaService.ActualizarEstado(id, estado);
        return RedirectToAction("Cita");
    }
}
