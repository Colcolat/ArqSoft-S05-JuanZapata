using CitasApp.Domain.Interfaces;
using CitasApp.Models;
using Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonMedicoRepository : IMedicoRepository
    {
        public List<Medico> ObtenerTodos()
        {
            var db = JsonDb.CargarDatos();
            return db.Medicos;
        }

        public Medico? ObtenerPorId(int id)
        {
            var db = JsonDb.CargarDatos();
            return db.Medicos.FirstOrDefault(m => m.Id == id);
        }
    }
}