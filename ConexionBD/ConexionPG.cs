using ConexionBD;
using Npgsql;
using System;

public class ConexionPostgreSQL : IConexion
{
    private string _dbConnectionStringBuilder;
    private string _commandString;
    private NpgsqlConnection _conexion;

    // Constructor para inicializar la cadena de conexión
    public ConexionPostgreSQL(string host, string database, string username, string pass, string port = "5432")
    {
        _dbConnectionStringBuilder = $"Host={host};Database={database};Username={username};Password={pass};Port={port};";
    }

    // Método para abrir la conexión
    public void OpenConnection()
    {
        if (_conexion == null)
        {
            _conexion = new NpgsqlConnection(_dbConnectionStringBuilder);
        }

        try
        {
            if (_conexion.State == System.Data.ConnectionState.Closed)
            {
                _conexion.Open();
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
            throw;
        }
    }

    // Método para cerrar la conexión
    public void CloseConnection()
    {
        if (_conexion != null && _conexion.State == System.Data.ConnectionState.Open)
        {
            try
            {
                _conexion.Close();
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error al cerrar la conexión: {ex.Message}");
                throw;
            }
        }
    }

    // Método para realizar una consulta SELECT
    public string Select(string table, string[] columns)
    {
        // Construir la cadena de columnas para la consulta
        string columnString = string.Join(", ", columns);
        _commandString = $"SELECT {columnString} FROM {table};";
        Console.WriteLine(_commandString);

        try
        {
            // Abrir la conexión
            OpenConnection();

            // Crear un comando con la consulta
            NpgsqlCommand cmd = new NpgsqlCommand(_commandString, _conexion);

            // Ejecutar el comando y obtener los resultados
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    // Imprimir cada columna
                    Console.Write(rdr[i] + (i < columns.Length - 1 ? " -- " : ""));
                }
                Console.WriteLine();
            }
            rdr.Close();
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine("Error en la query: " + ex.Message);
            throw;
        }
        finally
        {
            // Asegurarse de cerrar la conexión al finalizar
            CloseConnection();
        }

        return "Consulta completada";
    }

    public System.Data.ConnectionState getStatus()
    {
        return _conexion.State;
    }
}
