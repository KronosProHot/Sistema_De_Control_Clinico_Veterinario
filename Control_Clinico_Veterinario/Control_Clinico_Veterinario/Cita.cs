using System;

namespace SistemaVeterinaria
{
    public class Cita
    {
        public DateTime FechaHora { get; set; }
        public Cliente Cliente { get; set; }
        public Mascota Mascota { get; set; }
        public string Estado { get; set; }

        public Cita(DateTime fechaHora, Cliente cliente, Mascota mascota)
        {
            FechaHora = fechaHora;
            Cliente = cliente;
            Mascota = mascota;
            Estado = "Pendiente";
        }

        public void RegistrarLlegada()
        {
            Estado = "En Sala de Espera";
            Console.WriteLine($"\n--> {Mascota.Nombre} ha llegado y está en sala de espera.");
        }
    }
}