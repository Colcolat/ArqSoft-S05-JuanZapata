namespace CitasApp.Controllers;
using  Microsoft.AspNetCore.Mvc;

public class PacienteController : Controller
{
    public IActionResult Paciente()
    {
        return View();
    }
    
}