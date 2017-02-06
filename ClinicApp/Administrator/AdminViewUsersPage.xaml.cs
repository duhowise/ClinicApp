using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClinicApp.Data;
using ClinicModel;

namespace ClinicApp.Administrator
{
    /// <summary>
    /// Interaction logic for AdminViewUsersPage.xaml
    /// </summary>
    public partial class AdminViewUsersPage : Page
    {
        private List<Staff> _users;

        public AdminViewUsersPage()
        {
            InitializeComponent();
            Loaded += AdminViewUsersPage_Loaded;
        }

        private async void AdminViewUsersPage_Loaded(object sender, RoutedEventArgs e)
        {
            _users =new List<Staff>();
             _users= UserRepository.GetAllUsers() as List<Staff>;

            StaffList.ItemsSource = _users;
        }
    }
}
