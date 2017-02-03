using System.Configuration;

namespace ClinicApp.Data
{
    public class ConnectionHelper
    {

        public string ConnectionString =ConfigurationManager.ConnectionStrings["ClinicConnection"].ConnectionString;

    }
}
