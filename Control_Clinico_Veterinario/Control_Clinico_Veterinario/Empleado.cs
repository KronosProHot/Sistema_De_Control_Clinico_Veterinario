using System;

namespace SistemaVeterinaria
{
    public class Empleado : Persona
    {
        public string Rol { get; set; }

        public Empleado(string nombre, string telefono, string rol) : base(nombre, telefono)
        {
            Rol = rol;
        }

        // Implementación polimórfica
        public override void MostrarDetalles()
        {
            Console.WriteLine($"[EMPLEADO] {Nombre} | Cargo: {Rol} | Tel: {Telefono}");
        }
    }
}