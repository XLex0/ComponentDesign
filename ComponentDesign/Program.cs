using System;

namespace ComponentDesign
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crear una instancia de ConexionMySql
            ConexionMySql mysql = new ConexionMySql(
                "bcvvglp6byv7i4poecqn-mysql.services.clever-cloud.com",
                "bcvvglp6byv7i4poecqn",
                "utharwr7hzo8ft39",
                "vvPhcTSX7mpSNCqNJ8mm",
                "3306"
            );

            try
            {
                // Abrir la conexión
                mysql.OpenConnection();
                Console.WriteLine("Conexión a la base de datos abierta exitosamente.");
                mysql.Select("Cliente", ["Nombre","Apellido","DNI"]);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión
                mysql.CloseConnection();
                Console.WriteLine("Conexión a la base de datos cerrada.");
            }

            // Esperar a que el usuario presione una tecla antes de cerrar la aplicación
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
