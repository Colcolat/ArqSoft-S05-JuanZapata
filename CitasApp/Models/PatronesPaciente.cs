using System;
using System.Collections.Generic;
using System.Linq;
using CitasApp.Data;

namespace CitasApp.Models
{
    // ==========================================
    // PATRÓN OBSERVER
    // ==========================================
    public interface IPacienteObserver
    {
        void NotificarNuevoPaciente(Paciente paciente);
    }

    public class EmailNotificador : IPacienteObserver
    {
        public void NotificarNuevoPaciente(Paciente paciente)
        {
            Console.WriteLine($"[EMAIL] Bienvenida enviada al paciente {paciente.Id} - {paciente.Nombre} {paciente.Apellido} al correo {paciente.Email}");
        }
    }

    public class LogNotificador : IPacienteObserver
    {
        public void NotificarNuevoPaciente(Paciente paciente)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Nuevo paciente registrado: {paciente.Nombre} {paciente.Apellido}");
        }
    }

    public class PacienteManager
    {
        private List<IPacienteObserver> _observadores = new List<IPacienteObserver>();

        public void AgregarObservador(IPacienteObserver observador)
        {
            _observadores.Add(observador);
        }

        public void RegistrarPaciente(Paciente paciente)
        {
            // Guardar en la base de datos JSON
            var db = JsonDb.CargarDatos();
            paciente.Id = db.Pacientes.Any() ? db.Pacientes.Max(p => p.Id) + 1 : 1;
            db.Pacientes.Add(paciente);
            JsonDb.GuardarDatos(db);

            // Notificar a todos los observadores
            foreach (var obs in _observadores)
            {
                obs.NotificarNuevoPaciente(paciente);
            }
        }
    }

    // ==========================================
    // PATRÓN DECORATOR
    // ==========================================
    public abstract class PacienteDecorator : IPaciente
    {
        protected IPaciente _paciente;

        public PacienteDecorator(IPaciente paciente)
        {
            _paciente = paciente;
        }

        public virtual string ObtenerDescripcion()
        {
            return _paciente.ObtenerDescripcion();
        }
    }

    public class PacienteVIP : PacienteDecorator
    {
        public PacienteVIP(IPaciente paciente) : base(paciente) { }

        public override string ObtenerDescripcion()
        {
            return _paciente.ObtenerDescripcion() + " [Atención VIP]";
        }
    }

    public class PacienteUrgente : PacienteDecorator
    {
        public PacienteUrgente(IPaciente paciente) : base(paciente) { }

        public override string ObtenerDescripcion()
        {
            return _paciente.ObtenerDescripcion() + " [Atención Urgente]";
        }
    }

    // ==========================================
    // PATRÓN FACTORY
    // ==========================================
    public class PacienteFactory
    {
        // Crea el modelo base de paciente
        public static Paciente CrearPaciente(string nombre, string apellido, string email, string telefono)
        {
            return new Paciente
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Telefono = telefono
            };
        }

        // Crea el paciente decorado según el tipo especificado
        public static IPaciente CrearPacienteConDecorador(Paciente pacienteBase, string tipo)
        {
            if (string.IsNullOrEmpty(tipo)) return pacienteBase;

            return tipo.ToLower() switch
            {
                "vip" => new PacienteVIP(pacienteBase),
                "urgente" => new PacienteUrgente(pacienteBase),
                _ => pacienteBase,
            };
        }
    }
}
