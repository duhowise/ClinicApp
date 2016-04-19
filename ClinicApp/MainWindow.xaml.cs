using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int Id;
        public string fullname = null;
        private int valid = 0;
        public MainWindow()
        {
            InitializeComponent();

        }

        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }

        public string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //DialogResult dr= new DialogResult();
            var response = System.Windows.MessageBox.Show("Do you really want to close the Application", "Exit",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(userName.Text) || string.IsNullOrEmpty(Password.Password))
            {
                System.Windows.MessageBox.Show("All Details Re Required", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Password.Password = "";
                userName.Text = "";
            }
            else
            {
                valid = Clinic.VerifyUser(userName.Text, Password.Password);

                if (valid == 1)
                {
                    ID = Clinic.USerType(userName.Text, Password.Password);
                    FullName = Clinic.Username(userName.Text, Password.Password);
                    ClinicAdmin admin =new ClinicAdmin();
                    admin.Show();
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

    }
}
