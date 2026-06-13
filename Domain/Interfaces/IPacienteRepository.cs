namespace CitasApp.Domain.Interfaces;

using CitasApp.Models;

public interface IPacienteRepository
{
    List<Paciente> ObtenerTodos();
    Paciente? ObtenerPorId(int id);
}