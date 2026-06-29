using Microsoft.AspNetCore.Mvc;
using CitasApp.Models;
using CitasApp.Data;
using System.Linq;

namespace CitasApp.Controllers
{
    [Route("api/pacientes")]
    [ApiController]
    public class PacientesApiController : ControllerBase
    {
        private static PacienteManager _manager = new PacienteManager();

        static PacientesApiController()
        {
            // Registrar los observadores
            _manager.AgregarObservador(new EmailNotificador());
            _manager.AgregarObservador(new LogNotificador());
        }

        // GET: api/pacientes
        [HttpGet]
        public IActionResult Get()
        {
            var db = JsonDb.CargarDatos();
            return Ok(db.Pacientes);
        }
        
        // GET: api/pacientes/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var db = JsonDb.CargarDatos();
            var p = db.Pacientes.FirstOrDefault(x => x.Id == id);
            
            if (p == null) 
            {
                return NotFound(new { mensaje = $"No se encontró el paciente con Id {id}" });
            }
            
            return Ok(p);
        }
        
        // POST: api/pacientes
        [HttpPost]
        public IActionResult Post([FromBody] PacienteDto dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre) || string.IsNullOrEmpty(dto.Apellido))
            {
                return BadRequest(new { mensaje = "El nombre y apellido son obligatorios." });
            }

            // PATRÓN FACTORY: Crea la entidad base
            var nuevoPaciente = PacienteFactory.CrearPaciente(dto.Nombre, dto.Apellido, dto.Email, dto.Telefono);
            
            // PATRÓN OBSERVER: Se registra en DB y notifica a los observadores
            _manager.RegistrarPaciente(nuevoPaciente);
            
            // PATRÓN DECORATOR: Si el DTO trae un tipo ("VIP", "Urgente"), se decora
            var pacienteDecorado = PacienteFactory.CrearPacienteConDecorador(nuevoPaciente, dto.Tipo);
            
            return Ok(new { 
                mensaje = "Paciente registrado exitosamente", 
                descripcion = pacienteDecorado.ObtenerDescripcion(),
                paciente = nuevoPaciente 
            });
        }
    }

    public class PacienteDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Tipo { get; set; } // Puede ser "VIP", "Urgente" o vacío
    }
}
