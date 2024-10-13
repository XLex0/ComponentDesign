using System;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConexionBD;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ConexionPG : IConexion
{
    private string _conexString;
    private string _commandString;
    private NpgsqlConnection conex = new NpgsqlConnection();
    public ConexionPG(string servidor, string database, string username, string password, string port)
    {
        _conexString = $"Server={servidor};Database={database};User={username};Password={password};Port={port};";

    }
    public NpgsqlConnection OpenConnection()
    {

        if (conex == null && _conexString != null) {
            conex.ConnectionString = _conexString;
        }

        try {
            conex.Open();
            MessageBox.Show("Se conectó correctamente a la base de datos")
           }

        catch (NpsqlException e) {
            MessageBox.Show($"No se pudo conectar a la base de datos: {e.Message}")
           }

        return conex;

    }
    public void ClosedConnection() {
        if (conex != null && conex.State == System.Data.ConnectionState.Open)
        {
            try
            {
                conex.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error al cerrar la conexión: {ex.Message}");
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
            MySqlCommand cmd = new MySqlCommand(_commandString, _conexion);
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


        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error en la query" + ex);
            return "";
        }

    }
