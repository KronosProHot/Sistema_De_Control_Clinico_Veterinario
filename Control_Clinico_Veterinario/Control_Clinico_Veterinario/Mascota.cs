namespace SistemaVeterinaria
{
    public class Mascota
    {
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public Cliente Tutor { get; set; }

        public Mascota(string nombre, string especie, Cliente tutor)
        {
            Nombre = nombre;
            Especie = especie;
            Tutor = tutor;
        }
    }
}