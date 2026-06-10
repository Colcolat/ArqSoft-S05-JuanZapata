using System.Text.Json;
using CitasApp.Models;

namespace Infrastructure.Data
{
    public static class JsonDb
    {
    
        private static string filePath = "datos.json";

        public class DatabaseSchema
        {
            public List<Paciente> Pacientes { get; set; } = new();
            public List<Medico> Medicos { get; set; } = new();
            public List<Cita> Citas { get; set; } = new();
        }

        public static DatabaseSchema CargarDatos()
        {
            if (!File.Exists(filePath))
            {
                return new DatabaseSchema(); 
            }

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<DatabaseSchema>(jsonString) ?? new DatabaseSchema();
        }

        public static void GuardarDatos(DatabaseSchema db)
        {
            string jsonString = JsonSerializer.Serialize(db, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }
    }
}