using System.Windows;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicApp.Doctor;
using ClinicApp.Logic;
using ClinicApp.Nurse;
using ClinicApp.Pharmacist;
using ClinicModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        User loggedinUser=new User();
        CMB cmb = new CMB();
        private static int _id;
        private static string _fullname = null;
   
        public MainWindow()
        {
            InitializeComponent();
          

        }

        public static int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public static string FullName
        {
            get { return _fullname; }
            set { _fullname = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            LogUserIn();

        }

        public async void LogUserIn()
        {
            if (string.IsNullOrWhiteSpace(userName.Text) || string.IsNullOrWhiteSpace(Password.Password))
            {
                await this.ShowMessageAsync("Sorry 'bou that !", "All details are required");
                Password.Password = "";
                userName.Text = "";
                userName.Focus();
            }
            else
            {
                var user = new User();
                user.UserName = userName.Text;
                user.Password = Password.Password;
                var valid=new UserRepository().VerifyUser(user);
                loggedinUser = new UserRepository().Login(user);
                if (valid==1)
                {
                    ID =loggedinUser.Id;
                    FullName = loggedinUser.FirstName+" "+loggedinUser.LastName.ToUpper();
                    if (ID == 1)
                    {
                        DocAdmin doctor = new DocAdmin();
                        doctor.Show();
                    }
                    else if (ID == 2)
                    {
                        NurAdmin nurse = new NurAdmin();
                        nurse.Show(); 
                    }
                    else if (ID == 3)
                    {
                        PharAdmin pharmacist = new PharAdmin();
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
