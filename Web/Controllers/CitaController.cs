using Microsoft.AspNetCore.Mvc;
using CitasApp.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Web.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;


        public CitaController(
            ICitaRepository citaRepository, 
            IPacienteRepository pacienteRepository, 
            IMedicoRepository medicoRepository)
        {
            _citaRepository = citaRepository;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
        }

        public IActionResult Index()
        {
            ViewBag.Pacientes = _pacienteRepository.ObtenerTodos();
            ViewBag.Medicos = _medicoRepository.ObtenerTodos();
            
            var citas = _citaRepository.ObtenerTodos();
            return View(citas);
        }
        
        public IActionResult PorPaciente(int pacienteId)
        {
            // Hacemos lo mismo para la vista filtrada
            ViewBag.Pacientes = _pacienteRepository.ObtenerTodos();
            ViewBag.Medicos = _medicoRepository.ObtenerTodos();
            
            var citasDelPaciente = _citaRepository.ObtenerPorPaciente(pacienteId);
            return View(citasDelPaciente);
        }
    }
}