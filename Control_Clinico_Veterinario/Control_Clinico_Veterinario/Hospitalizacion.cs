using System;
using System.Collections.Generic;

namespace SistemaVeterinaria
{
    public class Hospitalizacion : ServicioMedico
    {
        public DateTime FechaIngreso { get; set; }
        public List<string> HistorialMonitoreo { get; set; }
        public bool AltaMedica { get; set; }

        // Costo base de hospitalización Q500.00
        public Hospitalizacion(Mascota paciente, Empleado veterinario) : base(paciente, veterinario, 500.00m)
        {
            FechaIngreso = DateTime.Now;
            HistorialMonitoreo = new List<string>();
            AltaMedica = false;
            Console.WriteLine($"\n--> Paciente {Paciente.Nombre} ingresado a hospitalización.");
        }

        public void RegistrarMonitoreo(string estado)
        {
            string registro = $"[{DateTime.Now.ToString("HH:mm")}] {estado}";
            HistorialMonitoreo.Add(registro);
            Console.WriteLine($"--> Monitoreo guardado: {estado}");
        }

        public override void FinalizarServicio()
        {
            AltaMedica = true;
            Console.WriteLine($"--> Paciente {Paciente.Nombre} dado de alta. Puede pasar a facturación.");
        }
    }
}