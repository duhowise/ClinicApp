using System.Configuration;
using System.Data.SqlClient;

namespace ClinicApp.Data
{
    public class ConnectionHelper
    {
        public string ConnectionString =
            ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString;

        //public string Connect() => ConnectionString;
    }
}