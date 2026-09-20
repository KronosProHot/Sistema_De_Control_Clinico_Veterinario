using System;

namespace SistemaVeterinaria
{
    public abstract class ServicioMedico
    {
        public Mascota Paciente { get; set; }
        public Empleado Veterinario { get; set; }
        public decimal Costo { get; set; }
        public bool Cobrado { get; set; }

        public ServicioMedico(Mascota paciente, Empleado veterinario, decimal costo)
        {
            Paciente = paciente;
            Veterinario = veterinario;
            Costo = costo;
            Cobrado = false;
        }

        public abstract void FinalizarServicio();
    }
}