namespace CitasApp.Domain.Interfaces;

using CitasApp.Models;

public interface ICitaRepository
{
    List<Cita> ObtenerTodos();
    Cita? ObtenerPorId(int id);
    List<Cita> ObtenerPorPaciente(int pacienteId);
}