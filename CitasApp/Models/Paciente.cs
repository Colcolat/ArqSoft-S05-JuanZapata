namespace CitasApp.Models
{
    public class Paciente : IPaciente
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }

        public String Email { get; set; }
        public String Telefono { get; set; }

        public virtual string ObtenerDescripcion()
        {
            return $"Paciente: {Nombre} {Apellido}";
        }
    }
}
