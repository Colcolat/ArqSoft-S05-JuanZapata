using CitasApp.Application.DTOs;
using CitasApp.Application.Interfaces;
using CitasApp.Domain.Interfaces;
using CitasApp.Models;

namespace CitasApp.Application.Services;

public class CitaService : ICitaService
{
    private readonly ICitaRepository _citaRepo;
    private readonly IPacienteRepository _pacienteRepo;
    private readonly IMedicoRepository _medicoRepo;

    public CitaService(ICitaRepository citaRepo, IPacienteRepository pacienteRepo, IMedicoRepository medicoRepo)
    {
        _citaRepo = citaRepo;
        _pacienteRepo = pacienteRepo;
        _medicoRepo = medicoRepo;
    }

    public List<CitaViewModel> GetAll()
    {
        var citas = _citaRepo.ObtenerTodos();
        var pacientes = _pacienteRepo.ObtenerTodos();
        var medicos = _medicoRepo.ObtenerTodos();

        return citas.Select(c => new CitaViewModel
        {
            Id = c.Id,
            NombrePaciente = pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Nombre + " " +
                             pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Apellido,
            NombreMedico = medicos.FirstOrDefault(m => m.Id == c.MedicoId)?.Nombre ?? string.Empty,
            Fecha = c.Fecha.ToString(),
            FechaHora = $"{c.Fecha} {c.Hora}",
            Motivo = c.Motivo,
            Estado = c.Estado
        }).ToList();
    }

    public CitaViewModel? GetById(int id)
    {
        var cita = _citaRepo.ObtenerPorId(id);
        if (cita == null) return null;

        var pacientes = _pacienteRepo.ObtenerTodos();
        var medicos = _medicoRepo.ObtenerTodos();
        var paciente = pacientes.FirstOrDefault(p => p.Id == cita.PacienteId);
        var medico = medicos.FirstOrDefault(m => m.Id == cita.MedicoId);

        return new CitaViewModel
        {
            Id = cita.Id,
            NombrePaciente = paciente?.Nombre + " " + paciente?.Apellido,
            NombreMedico = medico?.Nombre ?? string.Empty,
            Fecha = cita.Fecha.ToString(),
            FechaHora = $"{cita.Fecha} {cita.Hora}",
            Motivo = cita.Motivo,
            Estado = cita.Estado
        };
    }

    public void Add(Cita cita) => throw new NotImplementedException();
}
