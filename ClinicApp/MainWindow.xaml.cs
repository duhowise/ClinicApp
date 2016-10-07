using System.Windows;
using System.Windows.Input;
using ClinicApp.Doctor;
using ClinicApp.Logic;
using ClinicApp.Nurse;
using ClinicApp.Pharmacist;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogUserIn();
        }

        public async void LogUserIn()
        {
            if (string.IsNullOrEmpty(userName.Text) || string.IsNullOrEmpty(Password.Password))
            {
                await this.ShowMessageAsync("Sorry 'bou that !", "All details are required");
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
                    NurAdmin nurse=new NurAdmin();
                    if (Id == 1)
                    { doctor.Show(); }
                    else if (Id == 2)
                    {
                       nurse.Show(); 
                    }
                    else if (Id==3)
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
                    await this.ShowMessageAsync("Please Check Username or Password", "Login Failed");
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

        private void Password_KeyDown(object sender, KeyEventArgs e)
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

        private async void btnCantLogin_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            routedEventArgs.Handled = true;
            MessageDialogResult result = await this.ShowMessageAsync("Can't login with existing password?", "please contact the administrator at the ICT Directorate", MessageDialogStyle.AffirmativeAndNegative);
            
            if (result == MessageDialogResult.Negative)
            {
                routedEventArgs.Handled = false;
            }
            else
            {
                
            }
        }
    }
}
