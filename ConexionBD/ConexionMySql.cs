using ConexionBD;
using MySql.Data.MySqlClient;

public class ConexionMySql : IConexion
{

    private string _dbConnectionStringBuilder;
    private string _commandString;

    private MySqlConnection _conexion;


    public ConexionMySql(string host,string database, string username, string pass, string port = "3306")
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
            throw;
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
                throw;
            }
        }
    }

    public string Select(string table, string[] columns)

    {
        string columnString = string.Join(", ", columns);
        _commandString = $"SELECT {columnString} FROM {table};";
        Console.WriteLine(_commandString);


        try
        {
            OpenConnection();
            MySqlCommand cmd = new MySqlCommand( _commandString, _conexion);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    Console.Write(rdr[i] + (i < columns.Length - 1 ? " -- " : ""));
                }
                Console.WriteLine();
            }
            rdr.Close();


        } catch (MySqlException ex) {
            Console.WriteLine("Error en la query" + ex);
            throw;
        }
        return "Consulta completada";
    }

    public System.Data.ConnectionState getStatus()
    {
        return _conexion.State;
    }
}
