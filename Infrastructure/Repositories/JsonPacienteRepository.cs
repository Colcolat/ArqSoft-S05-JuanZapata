using CitasApp.Domain.Interfaces;
using CitasApp.Models;
using Infrastructure.Data; 

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonPacienteRepository : IPacienteRepository
    {
        public List<Paciente> ObtenerTodos()
        {
            var db = JsonDb.CargarDatos();
            return db.Pacientes;
        }

        public Paciente? ObtenerPorId(int id)
        {
            var db = JsonDb.CargarDatos();
            return db.Pacientes.FirstOrDefault(p => p.Id == id);
        }
    }
}