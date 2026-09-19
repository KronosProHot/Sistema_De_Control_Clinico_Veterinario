public class Factura {
    private Cliente cliente;
    private double total;
    private String metodoPago;
    private boolean pagada;

    public Factura(Cliente cliente, double total) {
        this.cliente = cliente;
        this.total = total;
        this.pagada = false;
    }

    public void procesarPago(String metodoPago) {
        this.metodoPago = metodoPago;
        this.pagada = true;
    }
}