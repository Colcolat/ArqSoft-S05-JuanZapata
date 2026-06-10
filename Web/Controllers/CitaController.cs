using Microsoft.AspNetCore.Mvc;
using CitasApp.Models;

namespace CitasApp.Controllers
{
    public class CitaController : Controller
    {

        private static List<Paciente> _pacientes = new List<Paciente>
        {
            new Paciente { Id = 1, Nombre = "Ana", Apellido = "García", Email = "ana@mail.com", Telefono = "555-0001" },
            new Paciente { Id = 2, Nombre = "Luis", Apellido = "Martínez", Email = "luis@mail.com", Telefono = "555-0002" },
            new Paciente { Id = 3, Nombre = "María", Apellido = "López", Email = "maria@mail.com", Telefono = "555-0003" }
        };

        private static List<Medico> _medicos = new List<Medico>
        {
            new Medico { Id = 1, Nombre = "Carlos", Apellido = "Reyes", Especialidad = "Medicina General", NumeroLicencia = "MG-10421" },
            new Medico { Id = 2, Nombre = "Patricia", Apellido = "Vega", Especialidad = "Pediatría", NumeroLicencia = "PD-20835" },
            new Medico { Id = 3, Nombre = "Roberto", Apellido = "Sánchez", Especialidad = "Cardiología", NumeroLicencia = "CA-30117" }
        };

        private static List<Cita> _citas = new List<Cita>
        {
            new Cita { Id = 1, PacienteId = 1, MedicoId = 1, Fecha = new DateOnly(2026, 6, 1), Hora = new TimeOnly(9, 0), Motivo = "Consulta general", Estado = "Confirmada" },
            new Cita { Id = 2, PacienteId = 2, MedicoId = 2, Fecha = new DateOnly(2026, 6, 1), Hora = new TimeOnly(10, 0), Motivo = "Revisión de resultados", Estado = "Pendiente" },
            new Cita { Id = 3, PacienteId = 3, MedicoId = 1, Fecha = new DateOnly(2026, 6, 3), Hora = new TimeOnly(11, 0), Motivo = "Primera consulta", Estado = "Pendiente" }
        };


        public IActionResult Index()
        {
            ViewBag.Pacientes = _pacientes;
            ViewBag.Medicos = _medicos;

            return View(_citas);
        }
        
        public IActionResult PorPaciente(int pacienteId)
        {
            ViewBag.Pacientes = _pacientes;
            ViewBag.Medicos = _medicos;
            
            var citasDelPaciente = _citas.Where(c => c.PacienteId == pacienteId).ToList();
            
            return View(citasDelPaciente);
        }
    }
}