using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ClinicModel;
using Dapper;

namespace ClinicApp.Data
{
    public class SupplierRepository
    {
        public IEnumerable<Supplier> GetAllSuppliers()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State ==ConnectionState.Closed)
                    connection.Open();
                return connection.Query<Supplier>("select * from Supplier");
            }
        }
        public Supplier GetSupplier(int id)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<Supplier>($"select * from where id={id} Supplier").SingleOrDefault();
            }
        }
        public Supplier GetSupplier(string name)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<Supplier>($"select * from where name={name} Supplier").SingleOrDefault();
            }
        }

    }
}