using System;
using System.Collections.Generic;

namespace SistemaVeterinaria
{
    public class Cliente : Persona
    {
        public List<Mascota> Mascotas { get; set; }

        public Cliente(string nombre, string telefono) : base(nombre, telefono)
        {
            Mascotas = new List<Mascota>();
        }

        public void RegistrarMascota(Mascota mascota)
        {
            Mascotas.Add(mascota);
            Console.WriteLine($"Mascota {mascota.Nombre} registrada al tutor {Nombre}.");
        }

        // Implementación polimórfica
        public override void MostrarDetalles()
        {
            Console.WriteLine($"[CLIENTE] {Nombre} | Tel: {Telefono} | Mascotas registradas: {Mascotas.Count}");
        }
    }
}