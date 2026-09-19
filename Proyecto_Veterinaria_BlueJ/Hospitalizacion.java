public class Hospitalizacion {
    private Mascota paciente;
    private String fechaIngreso;
    private String fechaAlta;
    private String estadoSignosVitales;

    public Hospitalizacion(Mascota paciente, String fechaIngreso) {
        this.paciente = paciente;
        this.fechaIngreso = fechaIngreso;
        this.estadoSignosVitales = "Estable";
    }

    public void actualizarMonitoreo(String estado) {
        this.estadoSignosVitales = estado;
    }

    public void darDeAlta(String fechaAlta) {
        this.fechaAlta = fechaAlta;
    }
}