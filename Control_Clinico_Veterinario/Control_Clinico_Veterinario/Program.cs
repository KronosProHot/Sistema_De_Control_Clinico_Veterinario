using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVeterinaria
{
    class Program
    {
        // Simuladores de Base de Datos
        static List<Cliente> clientesBD = new List<Cliente>();
        static List<Empleado> empleadosBD = new List<Empleado>();
        static List<Cita> citasBD = new List<Cita>();
        static List<Factura> facturasBD = new List<Factura>();

        // Esta lista polimórfica guardará tanto Consultas como Hospitalizaciones
        static List<ServicioMedico> serviciosBrindadosBD = new List<ServicioMedico>();

        static void Main(string[] args)
        {
            Empleado vet = new Empleado("Dr. Méndez", "1234-5678", "Médico Veterinario");
            Empleado recep = new Empleado("Ana López", "8765-4321", "Recepcionista");
            empleadosBD.Add(vet);
            empleadosBD.Add(recep);

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    SISTEMA DE CLÍNICA VETERINARIA");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Citas, Recepción y Registro");
                Console.WriteLine("2. Consultas Clínicas y Triaje");
                Console.WriteLine("3. Hospitalización y Monitoreo");
                Console.WriteLine("4. Facturación, Pagos y Caja");
                Console.WriteLine("5. Salir");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": SubMenuRecepcion(); break;
                    case "2": SubMenuConsultas(vet); break;
                    case "3": SubMenuHospitalizacion(vet); break;
                    case "4": SubMenuFacturacion(); break;
                    case "5": salir = true; break;
                }
            }
        }

        // ==========================================
        //        MÓDULO 1: CITAS Y RECEPCIÓN
        // ==========================================
        static void SubMenuRecepcion()
        {
            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("--- MÓDULO DE CITAS Y RECEPCIÓN ---");
                Console.WriteLine("1. Agendar Nueva Cita");
                Console.WriteLine("2. Recepción: Registrar Llegada");
                Console.WriteLine("3. Ver Registro General de Pacientes");
                Console.WriteLine("4. Volver");
                Console.Write("Seleccione: ");

                switch (Console.ReadLine())
                {
                    case "1": AgendarCita(); break;
                    case "2": RegistrarLlegada(); break;
                    case "3": VerRegistroPacientes(); break;
                    case "4": volver = true; break;
                }
            }
        }

        static void AgendarCita()
        {
            Console.Clear();
            Console.WriteLine("[ AGENDAR NUEVA CITA ]");
            Console.Write("Tutor: "); string nombreC = Console.ReadLine();
            Console.Write("Teléfono: "); string telC = Console.ReadLine();
            Cliente nuevoCliente = new Cliente(nombreC, telC);
            clientesBD.Add(nuevoCliente);

            Console.Write("Mascota: "); string nombreM = Console.ReadLine();
            Console.Write("Especie: "); string especieM = Console.ReadLine();
            Mascota nuevaMascota = new Mascota(nombreM, especieM, nuevoCliente);
            nuevoCliente.RegistrarMascota(nuevaMascota);

            Cita nuevaCita = new Cita(DateTime.Now, nuevoCliente, nuevaMascota);
            citasBD.Add(nuevaCita);
            Console.WriteLine("\nCita agendada.");
            Console.ReadKey();
        }

        static void RegistrarLlegada()
        {
            Console.Clear();
            var pendientes = citasBD.Where(c => c.Estado == "Pendiente").ToList();
            if (pendientes.Count == 0) { Console.WriteLine("No hay citas pendientes."); Console.ReadKey(); return; }

            for (int i = 0; i < pendientes.Count; i++)
                Console.WriteLine($"{i + 1}. {pendientes[i].Mascota.Nombre} ({pendientes[i].Cliente.Nombre})");

            Console.Write("\nSeleccione quién llegó: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= pendientes.Count)
                pendientes[idx - 1].RegistrarLlegada();
            Console.ReadKey();
        }

        static void VerRegistroPacientes()
        {
            Console.Clear();
            Console.WriteLine("[ ESTADO DE AGENDA ]");
            foreach (var cita in citasBD)
                Console.WriteLine($"- {cita.Mascota.Nombre} | Estado: {cita.Estado}");
            Console.ReadKey();
        }

        // ==========================================
        //        MÓDULO 2: CONSULTAS Y TRIAJE
        // ==========================================
        static void SubMenuConsultas(Empleado vet)
        {
            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("--- MÓDULO DE CONSULTAS ---");
                Console.WriteLine("1. Atender Paciente en Espera");
                Console.WriteLine("2. Internar Paciente (Enviar a Hospitalización)");
                Console.WriteLine("3. Volver");
                Console.Write("Seleccione: ");

                switch (Console.ReadLine())
                {
                    case "1": AtenderPaciente(vet); break;
                    case "2": InternarPaciente(vet); break;
                    case "3": volver = true; break;
                }
            }
        }

        static void AtenderPaciente(Empleado vet)
        {
            Console.Clear();
            var enEspera = citasBD.Where(c => c.Estado == "En Sala de Espera").ToList();
            if (enEspera.Count == 0) { Console.WriteLine("Nadie en sala de espera."); Console.ReadKey(); return; }

            for (int i = 0; i < enEspera.Count; i++)
                Console.WriteLine($"{i + 1}. {enEspera[i].Mascota.Nombre}");

            Console.Write("\nAtender a: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= enEspera.Count)
            {
                Cita citaActual = enEspera[idx - 1];
                Consulta nuevaConsulta = new Consulta(citaActual.Mascota, vet);

                Console.Write("Peso (kg): "); double peso = Convert.ToDouble(Console.ReadLine());
                Console.Write("Temp (°C): "); double temp = Convert.ToDouble(Console.ReadLine());
                nuevaConsulta.RealizarTriaje(peso, temp);

                Console.Write("Diagnóstico y Receta: ");
                nuevaConsulta.EmitirDiagnostico(Console.ReadLine(), "Ver notas");
                nuevaConsulta.FinalizarServicio();

                citaActual.Estado = "Atendido";
                serviciosBrindadosBD.Add(nuevaConsulta); // Se guarda para facturación
            }
            Console.ReadKey();
        }

        static void InternarPaciente(Empleado vet)
        {
            Console.Clear();
            Console.WriteLine("Seleccione mascota ya registrada para internar:");
            var pacientes = clientesBD.SelectMany(c => c.Mascotas).ToList();
            for (int i = 0; i < pacientes.Count; i++)
                Console.WriteLine($"{i + 1}. {pacientes[i].Nombre}");

            Console.Write("Mascota a internar: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= pacientes.Count)
            {
                Hospitalizacion hosp = new Hospitalizacion(pacientes[idx - 1], vet);
                serviciosBrindadosBD.Add(hosp); // Polimorfismo: Guardamos Hospitalización en la misma lista
            }
            Console.ReadKey();
        }

        // ==========================================
        //        MÓDULO 3: HOSPITALIZACIÓN
        // ==========================================
        static void SubMenuHospitalizacion(Empleado vet)
        {
            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("--- MÓDULO DE HOSPITALIZACIÓN ---");
                Console.WriteLine("1. Registrar Monitoreo (Signos vitales)");
                Console.WriteLine("2. Dar de Alta a Paciente");
                Console.WriteLine("3. Volver");
                Console.Write("Seleccione: ");

                switch (Console.ReadLine())
                {
                    case "1": MonitorearPaciente(); break;
                    case "2": DarDeAlta(); break;
                    case "3": volver = true; break;
                }
            }
        }

        static void MonitorearPaciente()
        {
            Console.Clear();
            // Buscamos solo los servicios que son Hospitalizacion y no tienen alta
            var internados = serviciosBrindadosBD.OfType<Hospitalizacion>().Where(h => !h.AltaMedica).ToList();

            if (internados.Count == 0) { Console.WriteLine("No hay pacientes internados."); Console.ReadKey(); return; }

            for (int i = 0; i < internados.Count; i++)
                Console.WriteLine($"{i + 1}. {internados[i].Paciente.Nombre} | Ingreso: {internados[i].FechaIngreso.ToShortDateString()}");

            Console.Write("\nSeleccione paciente para monitoreo: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= internados.Count)
            {
                Console.Write("Ingrese estado (ej. FC estable, comió bien): ");
                internados[idx - 1].RegistrarMonitoreo(Console.ReadLine());
            }
            Console.ReadKey();
        }

        static void DarDeAlta()
        {
            Console.Clear();
            var internados = serviciosBrindadosBD.OfType<Hospitalizacion>().Where(h => !h.AltaMedica).ToList();
            if (internados.Count == 0) { Console.WriteLine("No hay pacientes para dar de alta."); Console.ReadKey(); return; }

            for (int i = 0; i < internados.Count; i++)
                Console.WriteLine($"{i + 1}. {internados[i].Paciente.Nombre}");

            Console.Write("\nDar de alta al paciente número: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= internados.Count)
            {
                internados[idx - 1].FinalizarServicio();
            }
            Console.ReadKey();
        }

        // ==========================================
        //        MÓDULO 4: FACTURACIÓN Y CAJA
        // ==========================================
        static void SubMenuFacturacion()
        {
            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("--- MÓDULO DE FACTURACIÓN Y CAJA ---");
                Console.WriteLine("1. Generar Factura (Cobrar servicios pendientes)");
                Console.WriteLine("2. Cierre de Caja Diaria (Ver ingresos)");
                Console.WriteLine("3. Volver al Menú Principal");
                Console.Write("Seleccione: ");

                switch (Console.ReadLine())
                {
                    case "1": CobrarServicios(); break;
                    case "2": VerCierreCaja(); break;
                    case "3": volver = true; break;
                }
            }
        }

        static void CobrarServicios()
        {
            Console.Clear();
            Console.WriteLine("[ GENERAR FACTURA ]");

            if (clientesBD.Count == 0) { Console.WriteLine("No hay clientes registrados."); Console.ReadKey(); return; }

            for (int i = 0; i < clientesBD.Count; i++)
                Console.WriteLine($"{i + 1}. {clientesBD[i].Nombre}");

            Console.Write("\nSeleccione el tutor a cobrar: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= clientesBD.Count)
            {
                Cliente clienteSeleccionado = clientesBD[idx - 1];

                // Buscar servicios prestados a las mascotas de este cliente que NO hayan sido cobrados
                var serviciosPendientes = serviciosBrindadosBD
                    .Where(s => clienteSeleccionado.Mascotas.Contains(s.Paciente) && !s.Cobrado)
                    .ToList();

                if (serviciosPendientes.Count == 0)
                {
                    Console.WriteLine("\nEste cliente no tiene servicios médicos pendientes de pago.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\n--- SERVICIOS A COBRAR ---");
                foreach (var servicio in serviciosPendientes)
                {
                    string tipoServicio = servicio is Consulta ? "Consulta Médica" : "Hospitalización";
                    Console.WriteLine($"- {tipoServicio} ({servicio.Paciente.Nombre}): Q{servicio.Costo}");
                }

                Factura nuevaFactura = new Factura(clienteSeleccionado, serviciosPendientes);

                Console.Write($"\nSubtotal es Q{nuevaFactura.SubTotal}. ¿Desea aplicar % de descuento? (Ingrese 0 para ninguno): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal descuento) && descuento > 0)
                {
                    nuevaFactura.AplicarDescuento(descuento);
                }

                Console.Write("\n¿Desea procesar el pago ahora? (S/N): ");
                if (Console.ReadLine().ToUpper() == "S")
                {
                    Console.Write("Método de pago (Efectivo/Tarjeta): ");
                    nuevaFactura.ProcesarPago(Console.ReadLine());
                    facturasBD.Add(nuevaFactura); // Guardamos la factura en la base de datos
                }
            }
            Console.ReadKey();
        }

        static void VerCierreCaja()
        {
            Console.Clear();
            Console.WriteLine("[ CIERRE DE CAJA DIARIA ]\n");

            if (facturasBD.Count == 0)
            {
                Console.WriteLine("No se han registrado pagos el día de hoy.");
            }
            else
            {
                decimal totalIngresos = 0;

                Console.WriteLine("FACTURA | CLIENTE           | MÉTODO   | TOTAL");
                Console.WriteLine("--------------------------------------------------");
                foreach (var fac in facturasBD)
                {
                    Console.WriteLine($"No.{fac.NumeroFactura:D4} | {fac.Cliente.Nombre,-17} | {fac.MetodoPago,-8} | Q{fac.Total:F2}");
                    totalIngresos += fac.Total;
                }

                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"INGRESOS TOTALES DEL DÍA: Q{totalIngresos:F2}");
            }
            Console.ReadKey();
    }
    }
}