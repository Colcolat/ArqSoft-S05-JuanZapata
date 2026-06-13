using Microsoft.AspNetCore.Mvc;
using CitasApp.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _pacienteRepository;

        // El contenedor de dependencias inyectará el adaptador activo (JSON o CSV)
        public PacienteController(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public IActionResult Index()
        {
            var pacientes = _pacienteRepository.ObtenerTodos();
            return View(pacientes);
        }

        public IActionResult Detalle(int id)
        {
            var paciente = _pacienteRepository.ObtenerPorId(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }
    }
}