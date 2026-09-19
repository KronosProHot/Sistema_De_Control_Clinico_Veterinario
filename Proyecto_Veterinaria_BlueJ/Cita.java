public class Cita {
    private String fechaHora;
    private Cliente cliente;
    private Mascota mascota;
    private String estado; // Pendiente, Confirmada, Asistió

    public Cita(String fechaHora, Cliente cliente, Mascota mascota) {
        this.fechaHora = fechaHora;
        this.cliente = cliente;
        this.mascota = mascota;
        this.estado = "Pendiente";
    }
}