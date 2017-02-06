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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClinicApp.Administrator
{
    /// <summary>
    /// Interaction logic for AdminAddRolePage.xaml
    /// </summary>
    public partial class AdminAddRolePage : Page
    {
        public AdminAddRolePage()
        {
            InitializeComponent();
            btnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MetroWindow;

            if (!string.IsNullOrWhiteSpace(textBoxRoleName.Text))
            {
                try
                {

                    UserRepository.AddRole(new Role
                    {
                        Name = textBoxRoleName.Text
                    });
                    await window.ShowMessageAsync("Success !", $"{textBoxRoleName.Text} Role Added Successfully");

                }
                catch (Exception exception)
                {
                    await window.ShowMessageAsync("Something went wrong !", $"{exception.Message}");
                }
            }
        }
    }
}
