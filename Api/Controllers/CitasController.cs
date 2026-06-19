using Microsoft.AspNetCore.Mvc;
using CitasApp.Domain.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaRepository _repository;

        public CitasController(ICitaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.ObtenerTodos());

        [HttpGet("porpaciente/{pacienteId}")]
        public IActionResult PorPaciente(int pacienteId)
        {
            var citas = _repository.ObtenerPorPaciente(pacienteId);
            return citas.Count == 0 ? NotFound() : Ok(citas);
        }
    }
}