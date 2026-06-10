using Microsoft.AspNetCore.Mvc;
using CitasApp.Models;

namespace CitasApp.Controllers
{
    public class PacienteController : Controller
    {
        private static List<Paciente> _pacientes = new List<Paciente>
        {
            new Paciente { Id = 1, Nombre = "Ana", Apellido = "García", Email = "ana@mail.com", Telefono = "555-0001" },
            new Paciente { Id = 2, Nombre = "Luis", Apellido = "Martínez", Email = "luis@mail.com", Telefono = "555-0002" },
            new Paciente { Id = 3, Nombre = "María", Apellido = "López", Email = "maria@mail.com", Telefono = "555-0003" }
        };

        public IActionResult Index() 
        {
            return View(_pacientes);
        }

        public IActionResult Detalle(int id)
        {
            var paciente = _pacientes.FirstOrDefault(p => p.Id == id);
            return paciente == null ? NotFound() : View(paciente);
        }
    }
}