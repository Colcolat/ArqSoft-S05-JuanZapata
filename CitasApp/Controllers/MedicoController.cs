using CitasApp.Data; 
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers
{
    public class MedicoController : Controller
    {
        public IActionResult Index() 
        {
            var db = JsonDb.CargarDatos();
            return View(db.Medicos);
        }

        public IActionResult Detalle(int id)
        {
            var db = JsonDb.CargarDatos();
            var medico = db.Medicos.FirstOrDefault(p => p.Id == id);
            return medico == null ? NotFound() : View(medico);
        }
    }
}