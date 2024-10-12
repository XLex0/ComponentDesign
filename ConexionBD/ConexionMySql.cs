using ConexionBD;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using System.Runtime.InteropServices.JavaScript;

public class ConexionMySql : IConexion
{

    private string _dbConnectionStringBuilder;
    private string _commandString;

    private MySqlConnection _conexion;


    public ConexionMySql(string host,string database, string username, string pass, string port)
    {
        _dbConnectionStringBuilder = $"Server={host};Database={database};User={username};Password={pass};Port={port};";
    }

    public void OpenConnection()
    {
        if (_conexion == null)
        {
            _conexion = new MySqlConnection(_dbConnectionStringBuilder);
        }

        try
        {
            if (_conexion.State == System.Data.ConnectionState.Closed)
            {
                _conexion.Open();
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
        }
    }

    public void CloseConnection()
    {
        if (_conexion != null && _conexion.State == System.Data.ConnectionState.Open)
        {
            try
            {
                _conexion.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error al cerrar la conexión: {ex.Message}");
            }
        }
    }

}
