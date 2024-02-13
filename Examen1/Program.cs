using System;
using System.Globalization;

class Program
{
    static int[] numeroFactura = new int[15];
    static string[] numeroPlaca = new string[15];
    static DateTime[] fecha = new DateTime[15];
    static TimeSpan[] hora = new TimeSpan[15];
    static string[] tipoVehiculo = new string[15];
    static int[] numeroCaseta = new int[15];
    static double[] montoAPagar = new double[15];
    static double[] pagaCon = new double[15];
    static double[] vuelto = new double[15];
    static double[] montoPagaCliente = new double[15];

    static bool vectoresInicializados = false;
    static int contadorTransacciones = 0;
    static int indice = 0;
    static void Main()
    {
        int opcion;
        do
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine("-------------------------Menú Principal del Sistema-------------------------");
            Console.WriteLine("     -----------------------------Peaje 506-------------------------");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("1. Inicializar Vectores");
            Console.WriteLine("2. Ingresar Paso Vehicular");
            Console.WriteLine("3. Consulta de vehículos por Número de Placa");
            Console.WriteLine("4. Modificar Datos de Vehículos por Número de Placa");
            Console.WriteLine("5. Reporte Todos los Datos de los vectores");
            Console.WriteLine("6. Salir");
            Console.Write("Opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    InicializarVectores();
                    break;

                case 2:
                    IngresarPasoVehicular();
                    break;

                case 3:
                    ConsultarPorNumeroPlaca();
                    break;

                case 4:
                    ModificarPorNumeroPlaca();
                    break;

                case 5:
                    ReportarTodosDatos();
                    break;

                case 6:
                    Console.WriteLine("¡Hasta luego!");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                    break;
            }
        } while (opcion != 6);
    }
    static void InicializarVectores()
    {
        for (int i = 0; i < 15; i++)
        {
            numeroFactura[i] = 0; 
            numeroPlaca[i] = ""; 
            fecha[i] = DateTime.MinValue;
            hora[i] = TimeSpan.MinValue;
            tipoVehiculo[i] = ""; 
            numeroCaseta[i] = 0; 
            montoAPagar[i] = 0; 
            pagaCon[i] = 0; 
            vuelto[i] = 0; 
        }
        indice = 0;

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Vectores iniciados correctamente. Digite ENTER para continuar.");
        Console.ReadKey();
        Console.Clear();
    }
    static void IngresarPasoVehicular()
    {
        for (int i = 0; i < 15; i++)
        {
            if (indice >= 15)
            {
                Console.WriteLine("Vectores Llenos");
                return;
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("                Ingrese los datos solicitados          ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("-------------------Peaje 506---------------  ");
            Console.ForegroundColor = ConsoleColor.White;
                            //---------------Ingresar factura--------------
            Console.Write("Número de Factura: ");
            string entradaNumeroFactura = Console.ReadLine();
            if (int.TryParse(entradaNumeroFactura, out int numero))
            {
                numeroFactura[indice] = numero;
            }
            else
            {
                Console.WriteLine("Error: Por favor, ingrese un número válido.");
                continue;
            }
                                //--------- Ingresar Placa-------------
            Console.Write("Numero de placa: ");
            numeroPlaca[indice] = Console.ReadLine();
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Fecha (dd/mm/aaaa): ");
                string fechaInput = Console.ReadLine(); ;
                if (DateTime.TryParseExact(fechaInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaPago))
                {
                    fecha[indice] = fechaPago;
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Formato de fecha incorrecto. Intente nuevamente.");
                    Console.WriteLine("Precione ENTER para continuar.");
                    Console.ReadKey();
                }
            } while (true);
                                      //------------ Ingresar Hora------------
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Hora (hh:mm): ");

                string horaInput = Console.ReadLine();
                if (TimeSpan.TryParseExact(horaInput, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan horaPago))
                {
                    hora[indice] = horaPago;
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Formato de hora incorrecto. Intente nuevamente.");
                    Console.WriteLine("Precione ENTER para continuar.");
                    Console.ReadKey();
                }
            } while (true);
                                // ------------- Ingresar Tipo de Vehículo-------------
            Console.Write("Tipo de Vehículo (1=Moto, 2=Vehículo Liviano, 3=Camión o Pesado, 4=Autobús): ");
            if (int.TryParse(Console.ReadLine(), out int tipo) && tipo >= 1 && tipo <= 4)
            {              
                tipoVehiculo[indice] = tipo.ToString();               
                montoAPagar[indice] = AsignarMontoAPagar(tipo.ToString());
            }
            else
            {
                Console.WriteLine("Tipo de Vehículo no válido. Debe ser 1, 2, 3 o 4.");
                continue;
            }
                                       // Ingresar Número de Caseta
            Console.Write("Número de Caseta (1, 2, 3): ");
            if (int.TryParse(Console.ReadLine(), out int numCaseta))
            {
                if (numCaseta >= 1 && numCaseta <= 3)
                {
                    // Asignar el número de caseta al arreglo
                    numeroCaseta[indice] = numCaseta;
                }
                else
                {
                    Console.WriteLine("Número de Caseta no válido. Debe ser 1, 2 o 3.");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("Número de Caseta no válido. Debe ser 1, 2 o 3.");
                continue;
            }
                                 //----------- Ingresar Monto Paga Cliente----------
            while (true)
            {
                Console.Write("Paga Con: ");
                if (double.TryParse(Console.ReadLine(), out double montoPagaClienteInput))
                {
                    if (montoPagaClienteInput < montoAPagar[indice])
                    {
                        Console.WriteLine("El monto pagado por el cliente no puede ser menor al monto a pagar.");
                        continue;
                    }
                    else
                    {
                        montoPagaCliente[indice] = montoPagaClienteInput;
                        // Calcular el vuelto
                        vuelto[indice] = montoPagaClienteInput - montoAPagar[indice];
                        break; // Salir del bucle si el monto es válido
                    }
                }
                else
                {
                    Console.WriteLine("Formato de monto incorrecto. Intente nuevamente.");
                }
            }
            indice++;
            Console.Clear(); 

                                  // ------------Mostrar los datos ingresados----------
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("             Datos del Paso Vehicular             ");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine($"Número de Factura: {numeroFactura[indice - 1]}");
            Console.WriteLine($"Número de Placa: {numeroPlaca[indice - 1]}");
            Console.WriteLine($"Fecha: {fecha[indice - 1].ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Hora: {hora[indice - 1].ToString("hh\\:mm")}");
            Console.WriteLine($"Tipo de Vehículo: {(tipoVehiculo[indice - 1] == "1" ? "Moto" : tipoVehiculo[indice - 1] == "2" ? "Vehículo Liviano" : tipoVehiculo[indice - 1] == "3" ? "Camión o Pesado" : "Autobús")}");
            Console.WriteLine($"Número de Caseta: {numeroCaseta[indice - 1]}");
            Console.WriteLine($"Monto a Pagar: {montoAPagar[indice - 1]}");
            Console.WriteLine($"Paga con: {montoPagaCliente[indice - 1]}");
            Console.WriteLine($"Vuelto: {vuelto[indice - 1]}");

            Console.WriteLine("--------------------------------------------------");

                            // ----------------Verificar si se desea continuar ingresando datos-----------
            Console.Write("Desea continuar (S/N)?: ");
            string respuesta = Console.ReadLine().ToUpper();
            if (respuesta != "S")
            {
                break;
            }
            Console.Clear();
        }
    }
    static double AsignarMontoAPagar(string tipoVehiculo)
    {
        switch (tipoVehiculo)
        {
            case "1": // Moto
                return 500;
            case "2": // Vehículo Liviano
                return 700;
            case "3": // Camión o Pesado
                return 2700;
            case "4": // Autobús
                return 3700;
            default:
                return 0; // Tipo de vehículo no válido
        }
    }
    static void ConsultarPorNumeroPlaca()
    {
        Console.WriteLine("Ingrese el número de placa a buscar:");
        string placaABuscar = Console.ReadLine().Trim(); // Eliminar espacios en blanco al final
        bool encontrado = false;
        for (int i = 0; i < indice; i++)
        {
            if (numeroPlaca[i].Trim() == placaABuscar) // Comparar eliminando espacios en blanco al final
            {
                encontrado = true;
                Console.Clear();
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("             Datos del Paso Vehicular             ");
                Console.WriteLine("--------------------------------------------------");

                Console.WriteLine($"Número de Factura: {numeroFactura[i]}");
                Console.WriteLine($"Número de Placa: {numeroPlaca[i]}");
                Console.WriteLine($"Fecha: {fecha[i].ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Hora: {hora[i].ToString("hh\\:mm")}");
                Console.WriteLine($"Tipo de Vehículo: {(tipoVehiculo[i] == "1" ? "Moto" : tipoVehiculo[i] == "2" ? "Vehículo Liviano" : tipoVehiculo[i] == "3" ? "Camión o Pesado" : "Autobús")}");
                Console.WriteLine($"Número de Caseta: {numeroCaseta[i]}");
                Console.WriteLine($"Monto a Pagar: {montoAPagar[i]}");
                Console.WriteLine($"Paga con: {montoPagaCliente[i]}");
                Console.WriteLine($"Vuelto: {vuelto[i]}");
                Console.WriteLine("--------------------------------------------------");
            }
        }
        if (!encontrado)
        {
            Console.WriteLine("No se encontraron datos para la placa ingresada.");
        }

        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }
    static void MostrarDatosVehiculo(int indice)
    {
        Console.WriteLine($"Número de Factura: {numeroFactura[indice]}");
        Console.WriteLine($"Número de Placa: {numeroPlaca[indice]}");
        Console.WriteLine($"Fecha: {fecha[indice].ToString("dd/MM/yyyy")}");
        Console.WriteLine($"Hora: {hora[indice].ToString("hh\\:mm")}");
        Console.WriteLine($"Tipo de Vehículo: {(tipoVehiculo[indice] == "1" ? "Moto" : tipoVehiculo[indice] == "2" ? "Vehículo Liviano" : tipoVehiculo[indice] == "3" ? "Camión o Pesado" : "Autobús")}");
        Console.WriteLine($"Número de Caseta: {numeroCaseta[indice]}");
        Console.WriteLine($"Monto a Pagar: {montoAPagar[indice]}");
        Console.WriteLine($"Paga con: {montoPagaCliente[indice]}");
        Console.WriteLine($"Vuelto: {vuelto[indice]}");
    }
    static int BuscarIndicePorPlaca(string placa)
    {
        for (int i = 0; i < indice; i++)
        {
            if (numeroPlaca[i] == placa)
            {
                return i; // Retorna el índice del vehículo si se encuentra
            }
        }
        return -1; // Retorna -1 si no se encuentra la placa en el arreglo
    }
    static void ModificarPorNumeroPlaca()
    {
        Console.Clear();
        Console.WriteLine("----------------- Modificar Datos por Número de Placa -----------------");
        Console.Write("Ingrese el número de placa del vehículo que desea modificar: ");
        string placaBuscada = Console.ReadLine();

                      //--------- Buscar el índice del vehículo con el número de placa ingresado-----
        int indiceVehiculo = BuscarIndicePorPlaca(placaBuscada);

        if (indiceVehiculo == -1)
        {
            Console.WriteLine("No se encontró ningún vehículo con el número de placa ingresado.");
            return;
        }
        Console.WriteLine("Datos del vehículo encontrado:");
        Console.WriteLine("--------------------------------");
        MostrarDatosVehiculo(indiceVehiculo);

                     // --------Menú de selección para modificar datos----------
        Console.WriteLine("Seleccione qué dato desea modificar:");
        Console.WriteLine("1. Número de Factura");
        Console.WriteLine("2. Tipo de Vehículo");
        Console.WriteLine("3. Número de Caseta");
        Console.WriteLine("4. Monto a Pagar");
        Console.WriteLine("5. Volver al Menú Principal");

        Console.Write("Opción: ");
        int opcion = int.Parse(Console.ReadLine());
        switch (opcion)
        {
            case 1:               
                Console.Write("Ingrese el nuevo número de factura: ");
                int nuevoNumeroFactura = int.Parse(Console.ReadLine());
                numeroFactura[indiceVehiculo] = nuevoNumeroFactura;
                break;
            case 2:               
                Console.Write("Ingrese el nuevo tipo de vehículo (1=Moto, 2=Vehículo Liviano, 3=Camión o Pesado, 4=Autobús): ");
                string nuevoTipoVehiculo = Console.ReadLine();
                tipoVehiculo[indiceVehiculo] = nuevoTipoVehiculo;              
                montoAPagar[indiceVehiculo] = AsignarMontoAPagar(nuevoTipoVehiculo);
                break;
            case 3:               
                Console.Write("Ingrese el nuevo número de caseta (1, 2, 3): ");
                int nuevoNumeroCaseta = int.Parse(Console.ReadLine());
                numeroCaseta[indiceVehiculo] = nuevoNumeroCaseta;
                break;
            case 4:               
                Console.Write("Ingrese el nuevo monto a pagar: ");
                double nuevoMontoAPagar = double.Parse(Console.ReadLine());
                montoAPagar[indiceVehiculo] = nuevoMontoAPagar;
                break;
            case 5:               
                Console.WriteLine("Volviendo al Menú Principal...");
                return;
            default:
                Console.WriteLine("Opción no válida. Volviendo al Menú Principal...");
                return;
        }

        Console.WriteLine("¡Datos modificados con éxito!");
    }
    static void ReportarTodosDatos()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("             Reporte de Todos los Datos             ");
        Console.WriteLine("--------------------------------------------------");
        for (int i = 0; i < 15; i++)
        {
            if (string.IsNullOrEmpty(numeroPlaca[i])) // ****Si no hay datos para este índice, omitir la impresión
                continue;

            Console.WriteLine($"Número de Factura: {numeroFactura[i]}");
            Console.WriteLine($"Número de Placa: {numeroPlaca[i]}");
            Console.WriteLine($"Fecha: {fecha[i].ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Hora: {hora[i].ToString("hh\\:mm")}");
            Console.WriteLine($"Tipo de Vehículo: {(tipoVehiculo[i] == "1" ? "Moto" : tipoVehiculo[i] == "2" ? "Vehículo Liviano" : tipoVehiculo[i] == "3" ? "Camión o Pesado" : "Autobús")}");
            Console.WriteLine($"Número de Caseta: {numeroCaseta[i]}");
            Console.WriteLine($"Monto a Pagar: {montoAPagar[i]:C}");
            Console.WriteLine($"Paga con: {montoPagaCliente[i]:C}");
            Console.WriteLine($"Vuelto: {vuelto[i]:C}");
            Console.WriteLine("--------------------------------------------------");
        }
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }

}
