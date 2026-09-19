public class Consulta {
    private Mascota paciente;
    private Empleado veterinario;
    private double peso;
    private double temperatura;
    private String diagnostico;
    private String recetaMedica;

    public Consulta(Mascota paciente, Empleado veterinario) {
        this.paciente = paciente;
        this.veterinario = veterinario;
    }

    public void realizarTriaje(double peso, double temperatura) {
        this.peso = peso;
        this.temperatura = temperatura;
    }

    public void emitirDiagnostico(String diagnostico, String recetaMedica) {
        this.diagnostico = diagnostico;
        this.recetaMedica = recetaMedica;
    }
}