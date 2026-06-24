using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories;

public static class RepositoryFactory
{
    public static IPacienteRepository CrearPacienteRepository(string entorno, IWebHostEnvironment env)
    {
        return entorno == "Production"
            ? new MemoriaPacienteRepository()
            : new JsonPacienteRepository(env);
    }
}
