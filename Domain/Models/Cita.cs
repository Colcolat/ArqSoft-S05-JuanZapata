namespace CitasApp.Domain.Models;

public class Cita
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
    public string Fecha { get; set; }
    public string FechaHora { get; set; }
    public string Motivo { get; set; }
    public string Estado { get; set; }
}