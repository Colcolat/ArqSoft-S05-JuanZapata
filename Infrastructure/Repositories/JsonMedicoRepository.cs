using System.Text.Json;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories;

public class JsonMedicoRepository : IMedicoRepository
{
    private readonly string _filePath;

    public JsonMedicoRepository(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(env.WebRootPath, "data", "Medicos.json");
    }

    public List<Medico> GetAll()
    {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Medico>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Medico>();
    }

    public Medico? GetById(int id) => GetAll().FirstOrDefault(m => m.Id == id);

    public void Add(Medico medico)
    {
        var medicos = GetAll();
        medico.Id = medicos.Max(m => m.Id) + 1;
        medicos.Add(medico);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(medicos, new JsonSerializerOptions { WriteIndented = true }));
    }
}
