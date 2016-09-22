namespace ClinicApp
{
    public class CurrentUserLoggedInData
    {
        private static int id;
        private static string firstName;
        private static string lastName;
        private static string userName;
        private static string password;
        private static int role;
        public static bool IsLoaded { get; set; }

        public static void ClearUserData()
        {
            Id = 0;
            FirstName = null;
            LastName = null;
            UserName = null;
            Role = 0;
        }

        public static int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public static string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public static string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        public static string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public static string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public static int Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
            }
        }

    }
}