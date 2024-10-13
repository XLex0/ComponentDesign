using System.Data.Common;

namespace ConexionBD
{
    public interface IConexion
    {
        

        public void OpenConnection();
        public void CloseConnection();

        public string Select(string table, string[] columns);

        public System.Data.ConnectionState getStatus();
    }
}
