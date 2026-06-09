using CitasApp.Application.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers;

public class MedicoController : Controller
{
    private readonly IMedicoService _medicoService;

    public MedicoController(IMedicoService medicoService)
    {
        _medicoService = medicoService;
    }

    public IActionResult Medico()
    {
        var medicos = _medicoService.GetAll();
        return View(medicos);
    }

    public IActionResult Detalle(int id)
    {
        var medico = _medicoService.GetById(id);
        if (medico == null) return Content("Medico no encontrado");
        return View(medico);
    }

    public IActionResult Nuevo()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Nuevo(Medico medico)
    {
        _medicoService.Add(medico);
        return RedirectToAction("Medico");
    }
}
