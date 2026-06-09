using System.Text.Json;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories;

public class JsonPacienteRepository : IPacienteRepository
{
    private readonly string _filePath;

    public JsonPacienteRepository(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(env.WebRootPath, "data", "Pacientes.json");
    }

    public List<Paciente> GetAll()
    {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Paciente>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Paciente>();
    }

    public Paciente? GetById(int id) => GetAll().FirstOrDefault(p => p.Id == id);

    public void Add(Paciente paciente)
    {
        var pacientes = GetAll();
        paciente.Id = pacientes.Max(p => p.Id) + 1;
        pacientes.Add(paciente);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(pacientes, new JsonSerializerOptions { WriteIndented = true }));
    }
}
