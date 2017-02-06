using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ClinicModel;
using Dapper;

namespace ClinicApp.Data
{
    public class UserRepository
    {
        public static IEnumerable<Staff> GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<Staff>("select * from staff");

            }

        }
        public static IEnumerable<Role> GetAllRoles()
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed||connection.State==ConnectionState.Broken)
                {
                    connection.Open();
                }
               return connection.Query<Role>($"select * from role");

            }

        }
        public static void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                connection.Query(@"INSERT INTO dbo.Users(FirstName,LastName ,UserName ,Password ,RoleId)VALUES(@FirstName,@LastName,@UserName,@Password,@RoleId)",user);
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

        public static int VerifyUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<User>($"select * from Users where username=@UserName and password=@Password", user).Count();

            }
        }
        public static User Login(User user)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.Query<User>($"select * from Users where username=@UserName and password=@Password", user).SingleOrDefault();

            }
        }


        public static void AddRole(Role role)
        {
            using (SqlConnection connection = new SqlConnection(new ConnectionHelper().ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                connection.Query(@"INSERT INTO dbo.Role( Name)VALUES(@Name)", role);
            }
        }
    }
}