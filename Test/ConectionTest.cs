using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;

namespace Tests
{
    [TestClass]
    public class ConectionTest { 

        private ConexionMySql _conexion;

        [TestInitialize]
        public void Setup()
        {
            _conexion = new ConexionMySql("junction.proxy.rlwy.net",
                "railway",
                "root",
                "agqfQjrsCnFdbNHQQpCODvuAnehscloT",
                "58029");
        }

        [TestMethod]
        public void TestOpenConnection_Success()
        {
            try
            {
                // Intenta abrir la conexión
                _conexion.OpenConnection();
                Assert.AreEqual(System.Data.ConnectionState.Open, _conexion.getStatus()); 
            }
            catch (Exception ex)
            {
                Assert.Fail($"Se lanzó una excepción al abrir la conexión: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión
                _conexion.CloseConnection();
            }
        }

        [TestMethod]
        public void TestOpenConnection_Failure()
        {
            // Cambia los parámetros a valores incorrectos para forzar un error
            var invalidConexion = new ConexionPostgreSQL("invalid_host", "invalid_database", "invalid_username", "invalid_password");

            try
            {
                invalidConexion.OpenConnection();
                Assert.Fail("Se esperaba una excepción al abrir la conexión con parámetros inválidos.");
            }
            catch (Exception ex)
            {
            }
        }

        [TestMethod]
        public void TestSelect_ValidQuery_ReturnsData()
        {
            _conexion.OpenConnection();

            try
            {

                var result = _conexion.Select("cliente", new[] { "Nombre", "Apellido", "DNI" });

            }
            catch(Exception ex) { Assert.Fail(ex.Message); }
          
        }

        [TestMethod]
        public void TestSelect_InvalidQuery_ThrowsException()
        {
            _conexion.OpenConnection();

            try
            {
                var result = _conexion.Select("InvalidTable", new[] { "ColumnaInexistente" });
                Assert.Fail("Se esperaba una excepción al ejecutar una consulta con tabla o columna inválida.");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _conexion.CloseConnection();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _conexion.CloseConnection();
        }
    }
}
