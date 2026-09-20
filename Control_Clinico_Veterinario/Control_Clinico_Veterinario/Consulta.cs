using System;

namespace SistemaVeterinaria
{
    public class Consulta : ServicioMedico
    {
        public double Peso { get; set; }
        public double Temperatura { get; set; }
        public string Diagnostico { get; set; }
        public string RecetaMedica { get; set; }

        // Se envía un costo base de Q150.00 a la clase padre
        public Consulta(Mascota paciente, Empleado veterinario) : base(paciente, veterinario, 150.00m) { }

        public void RealizarTriaje(double peso, double temperatura)
        {
            Peso = peso;
            Temperatura = temperatura;
            Console.WriteLine($"\n--> Triaje completado: Peso {peso}kg, Temp {temperatura}°C.");
        }

        public void EmitirDiagnostico(string diagnostico, string receta)
        {
            Diagnostico = diagnostico;
            RecetaMedica = receta;
            Console.WriteLine($"--> Diagnóstico emitido: {diagnostico}.");
        }

        public override void FinalizarServicio()
        {
            Console.WriteLine($"--> Consulta finalizada para {Paciente.Nombre}.");
        }
    }
}