using CitasApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers;

public class MedicoController :  Controller
{
    public IActionResult Medico()
    {
        return View();
    }
}