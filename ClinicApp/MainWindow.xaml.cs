using System.ComponentModel;
using System.Windows;
using ClinicApp.Administrator;
using ClinicApp.Data;
using ClinicApp.Doctor;
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
        BackgroundWorker loginBackgroundWorker=new BackgroundWorker();
        BackgroundWorker verifyWorker=new BackgroundWorker();
        User loggedinUser=new User();
        //CMB cmb = new CMB();
        private static int _id;
        private static string _fullname = null;
        private string _userName;
        private string _pass;

        public MainWindow()
        {
            InitializeComponent();
            userName.Focus();
            loginBackgroundWorker.WorkerSupportsCancellation = true;
            verifyWorker.WorkerSupportsCancellation = true;
            verifyWorker.RunWorkerCompleted += VerifyWorker_RunWorkerCompleted;
            verifyWorker.DoWork += VerifyWorker_DoWork;
            loginBackgroundWorker.RunWorkerCompleted += LoginBackgroundWorker_RunWorkerCompleted;
            loginBackgroundWorker.DoWork += LoginBackgroundWorker_DoWork;
        }

        private void VerifyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var user = new User();
            user.UserName = _userName;
            user.Password = _pass;
           
            e.Result=UserRepository.VerifyUser(user);

        }

        private async void VerifyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((int)e.Result!=1)
            {
                await this.ShowMessageAsync("Please Check Username or Password", "Login Failed");
                Password.Password = "";
                userName.Text = "";
                userName.Focus();
            }
            else
            {
                if (!loginBackgroundWorker.IsBusy)
                {
                    loginBackgroundWorker.RunWorkerAsync();
                }
            }
        }

        private void LoginBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LogUserIn();
        }
        private void LoginBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (ID)
            {
                case 1:
                    DocAdmin doctor = new DocAdmin();
                    doctor.Show();
                    break;
                case 2:
                    NurAdmin nurse = new NurAdmin();
                    nurse.Show();
                    break;
                case 3:
                    PharAdmin pharmacist = new PharAdmin();
                    pharmacist.Show();
                    break;
                case 4:
                    AdminMainWindow adminMain = new AdminMainWindow();
                    adminMain.Show();
                    break;
                default:
                    return;
                    
            }
            Hide();
            Password.Password = "";
            userName.Text = "";
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

        private async void Button_Click(object sender, RoutedEventArgs e)
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
                _userName = userName.Text;
                _pass = Password.Password;
                if (!verifyWorker.IsBusy)
                {
                    //Task task=new Task(verifyWorker.RunWorkerAsync);
                    //task.Start();
                    //await task;
                   verifyWorker.RunWorkerAsync();
                }
            }

        }

        public void LogUserIn()
        {
            var user = new User();
            user.UserName = _userName;
            user.Password = _pass;
          //todo Task<User>task=new Task<User>();
            loggedinUser =UserRepository.Login(user);
            ID = loggedinUser.RoleId;
            FullName = loggedinUser.FirstName + " " + loggedinUser.LastName.ToUpper();
             
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userName.Focus();
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
