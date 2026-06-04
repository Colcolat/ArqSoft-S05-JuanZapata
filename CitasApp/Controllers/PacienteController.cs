using CitasApp.Data; 
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers
{
    public class PacienteController : Controller
    {
        public IActionResult Index() 
        {
            var db = JsonDb.CargarDatos();
            return View(db.Pacientes);
        }

        public IActionResult Detalle(int id)
        {
            var db = JsonDb.CargarDatos();
            var paciente = db.Pacientes.FirstOrDefault(p => p.Id == id);
            return paciente == null ? NotFound() : View(paciente);
        }
    }
}