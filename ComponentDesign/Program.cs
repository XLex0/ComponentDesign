using System;

namespace ComponentDesign
{
    class Program
    {
        static void Main(string[] args)
        {
            // Puedes usar la interfaz IConexion para declarar la variable

            // Descomenta esta línea si deseas usar MySQL
            // conexion = new ConexionMySql(
            //     "bcvvglp6byv7i4poecqn-mysql.services.clever-cloud.com",
            //     "bcvvglp6byv7i4poecqn",
            //     "utharwr7hzo8ft39",
            //     "vvPhcTSX7mpSNCqNJ8mm",
            //     "3306"
            // );

            // Asignar la conexión de PostgreSQL
            var conexion = new ConexionPostgreSQL(
                "bnnpd4mx9zooee3tul0x-postgresql.services.clever-cloud.com",
                "bnnpd4mx9zooee3tul0x",
                "uvhngjuj8mhvb8pbmfun",
                "UhHtFhjDBwRzzOMCFMEtzz3107twHn",
                "50013"
            );


            try
            {
                // Abrir la conexión
                conexion.OpenConnection();
                Console.WriteLine("Conexión a la base de datos abierta exitosamente.");

                // Llamar al método Select usando la interfaz
                conexion.Select("Cliente", new string[] { "Nombre", "Apellido", "DNI" });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión
                conexion.CloseConnection();
                Console.WriteLine("Conexión a la base de datos cerrada.");
            }

            // Esperar a que el usuario presione una tecla antes de cerrar la aplicación
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}