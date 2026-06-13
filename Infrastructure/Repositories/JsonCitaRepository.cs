using CitasApp.Domain.Interfaces;
using CitasApp.Models;
using Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonCitaRepository : ICitaRepository
    {
        public List<Cita> ObtenerTodos()
        {
            var db = JsonDb.CargarDatos();
            return db.Citas;
        }

        public Cita? ObtenerPorId(int id)
        {
            var db = JsonDb.CargarDatos();
            return db.Citas.FirstOrDefault(c => c.Id == id);
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId)
        {
            var db = JsonDb.CargarDatos();
            return db.Citas.Where(c => c.PacienteId == pacienteId).ToList();
        }

        public void Agregar(Cita cita)
        {
            var db = JsonDb.CargarDatos();
            // Generamos un ID autoincremental
            cita.Id = db.Citas.Count > 0 ? db.Citas.Max(c => c.Id) + 1 : 1;
            
            db.Citas.Add(cita);
            JsonDb.GuardarDatos(db);
        }

        public void ConfirmarCita(int id)
        {
            var db = JsonDb.CargarDatos();
            var cita = db.Citas.FirstOrDefault(c => c.Id == id);
            
            if (cita != null)
            {
                cita.Estado = "Confirmada";
                JsonDb.GuardarDatos(db);
            }
        }
    }
}