using System.Data.Common;

namespace ConexionBD
{
    interface IConexion
    {

        public void OpenConnection();
        public void CloseConnection();

        public string Select(string table, string[] columns);


    }
}
