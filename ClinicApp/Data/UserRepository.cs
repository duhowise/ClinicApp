using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ClinicModel;
using Dapper;

namespace ClinicApp.Data
{
    public class UserRepository
    {
        public IEnumerable<User> GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<User>("select * from Users");

            }

        }
        public User GetUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<User>($"select * from Users where id={id}").SingleOrDefault();

            }
        }
        public User GetUser(string name)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<User>($"select * from Patient where name={name}").SingleOrDefault();

            }
        }
        public User Login(string username,string password)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<User>($"select * from Users where username=@Username and password=@password").SingleOrDefault();

            }
        }


    }
}