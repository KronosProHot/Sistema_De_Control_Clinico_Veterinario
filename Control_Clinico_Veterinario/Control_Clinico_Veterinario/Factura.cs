using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVeterinaria
{
    public class Factura
    {
        public int NumeroFactura { get; private set; }
        public DateTime FechaEmision { get; private set; }
        public Cliente Cliente { get; set; }
        public List<ServicioMedico> ServiciosCobrados { get; set; }
        public decimal SubTotal { get; private set; }
        public decimal Descuento { get; private set; }
        public decimal Total { get; private set; }
        public string MetodoPago { get; private set; }
        public bool Pagada { get; private set; }

        // Variable estática para generar números correlativos automáticamente
        private static int contadorFacturas = 1;

        public Factura(Cliente cliente, List<ServicioMedico> serviciosACobrar)
        {
            NumeroFactura = contadorFacturas++;
            FechaEmision = DateTime.Now;
            Cliente = cliente;
            ServiciosCobrados = serviciosACobrar;
            SubTotal = serviciosACobrar.Sum(s => s.Costo); // Suma automática
            Descuento = 0;
            Total = SubTotal;
            Pagada = false;
        }

        public void AplicarDescuento(decimal porcentaje)
        {
            if (porcentaje > 0 && porcentaje <= 100)
            {
                Descuento = SubTotal * (porcentaje / 100);
                Total = SubTotal - Descuento;
                Console.WriteLine($"--> Descuento del {porcentaje}% aplicado. Ahorro: Q{Descuento:F2}");
            }
        }

        public void ProcesarPago(string metodoPago)
        {
            MetodoPago = metodoPago;
            Pagada = true;

            // Cambiamos el estado de los servicios médicos a "Cobrado"
            foreach (var servicio in ServiciosCobrados)
            {
                servicio.Cobrado = true;
            }

            ImprimirRecibo();
        }

        private void ImprimirRecibo()
        {
            Console.WriteLine($"\n========================================");
            Console.WriteLine($" FACTURA NO. {NumeroFactura:D4} | FECHA: {FechaEmision.ToShortDateString()}");
            Console.WriteLine($" CLIENTE: {Cliente.Nombre}");
            Console.WriteLine($"----------------------------------------");
            Console.WriteLine($" SUBTOTAL:       Q{SubTotal:F2}");
            if (Descuento > 0)
            {
                Console.WriteLine($" DESCUENTO:     -Q{Descuento:F2}");
            }
            Console.WriteLine($" TOTAL A PAGAR:  Q{Total:F2}");
            Console.WriteLine($" MÉTODO DE PAGO: {MetodoPago.ToUpper()}");
            Console.WriteLine($" ESTADO:         CANCELADA");
            Console.WriteLine($"========================================\n");
        }
    }
}