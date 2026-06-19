using Microsoft.AspNetCore.Mvc;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteRepository _repository;

        public PacientesController(IPacienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.ObtenerTodos());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var paciente = _repository.ObtenerPorId(id);
            return paciente == null ? NotFound() : Ok(paciente);
        }
    }
}