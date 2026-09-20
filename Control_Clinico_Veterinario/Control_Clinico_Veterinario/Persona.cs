using System;

namespace SistemaVeterinaria
{
    public abstract class Persona
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        public Persona(string nombre, string telefono)
        {
            Nombre = nombre;
            Telefono = telefono;
        }

        // Método abstracto para aplicar polimorfismo en las clases hijas
        public abstract void MostrarDetalles();
    }
}