public class Mascota {
    private String nombre;
    private String especie;
    private Cliente tutor;

    public Mascota(String nombre, String especie, Cliente tutor) {
        this.nombre = nombre;
        this.especie = especie;
        this.tutor = tutor;
    }
}