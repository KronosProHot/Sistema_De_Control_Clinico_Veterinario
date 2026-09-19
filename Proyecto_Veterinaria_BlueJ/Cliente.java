import java.util.List;
import java.util.ArrayList;

public class Cliente {
    private String nombre;
    private String telefono;
    private List<Mascota> mascotas;

    public Cliente(String nombre, String telefono) {
        this.nombre = nombre;
        this.telefono = telefono;
        this.mascotas = new ArrayList<>();
    }

    public void registrarMascota(Mascota m) {
        this.mascotas.add(m);
    }
}