using Microsoft.AspNetCore.Mvc;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoRepository _repository;

        public MedicosController(IMedicoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.ObtenerTodos());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var medico = _repository.ObtenerPorId(id);
            return medico == null ? NotFound() : Ok(medico);
        }
    }
}