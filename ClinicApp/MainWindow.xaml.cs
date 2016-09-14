using System;
using System.Windows;
using System.Windows.Input;
using ClinicApp.Logic;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        CMB cmb = new CMB();
        public static int Id;
        public static string fullname = null;
        private int valid = 0;
        public MainWindow()
        {
            InitializeComponent();

        }

        public static int ID
        {
            get { return Id; }
            set { Id = value; }
        }

        public static string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //DialogResult dr= new DialogResult();
            //call confirmBox Here

            var response = System.Windows.MessageBox.Show("Do you really want to close the Application", "Exit",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           LogUserIn();
        }

        public void LogUserIn()
        {
            if (string.IsNullOrEmpty(userName.Text) || string.IsNullOrEmpty(Password.Password))
            {
                cmb.Message = "All Details Are Required";
                cmb.Show();

                ////System.Windows.MessageBox.Show("All Details Are Required", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Password.Password = "";
                userName.Text = "";
                userName.Focus();
            }
            else
            {
                valid = Clinic.VerifyUser(userName.Text, Password.Password);
                CurrentUserLoggedInData userData = new CurrentUserLoggedInData();
                if (valid == 1)
                {
                    ID = Clinic.USerType(userName.Text, Password.Password);

                    FullName = Clinic.Username(userName.Text, Password.Password);
                    PharAdmin pharmacist = new PharAdmin();
                    DocAdmin doctor = new DocAdmin();
                    if (Id == 1)
                    { doctor.Show(); }
                    else if (Id == 3)
                    {
                        pharmacist.Show();
                    }

                    Hide();
                    //System.Windows.MessageBox.Show("Login Sucessful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Password.Password = "";
                    userName.Text = "";
                }
                else
                {
                    System.Windows.MessageBox.Show("Please Check Username or Password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    Password.Password = "";
                    userName.Text = "";
                    userName.Focus();
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userName.Focus();
        }

        private void Password_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LogUserIn();
            }
        }

        private void userName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LogUserIn();
            }
        }
    }
}
