namespace CitasApp.Domain.Interfaces;

using CitasApp.Models;

public interface IMedicoRepository
{
    List<Medico> ObtenerTodos();
    Medico? ObtenerPorId(int id);
}