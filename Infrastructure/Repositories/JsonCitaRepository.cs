using System.Text.Json;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories;

public class JsonCitaRepository : ICitaRepository
{
    private readonly string _filePath;

    public JsonCitaRepository(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(env.WebRootPath, "data", "Citas.json");
    }

    public List<Cita> GetAll()
    {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Cita>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Cita>();
    }

    public Cita? GetById(int id) => GetAll().FirstOrDefault(c => c.Id == id);

    public void Add(Cita cita)
    {
        var citas = GetAll();
        cita.Id = citas.Max(c => c.Id) + 1;
        citas.Add(cita);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(citas, new JsonSerializerOptions { WriteIndented = true }));
    }
}
