using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows;
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
        public void AddNewSupplier(Supplier supplier)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
                {
                    if (connection.State ==ConnectionState.Closed)
                        connection.Open();
                    var query = @"INSERT INTO dbo.Supplier(Name,Address,Email,Phone)VALUES
                        (@name,@address,@email,@phone)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.Add("name", SqlDbType.NVarChar).Value =supplier.Name;
                    command.Parameters.Add("address", SqlDbType.NVarChar).Value = supplier.Address;
                    command.Parameters.Add("email", SqlDbType.NVarChar).Value = supplier.Email;
                    command.Parameters.Add("phone", SqlDbType.NVarChar).Value = supplier.Phone;
                    command.ExecuteNonQuery();
                    }
            }
            catch (Exception exception)
            {
              
            }
        }
    }
}