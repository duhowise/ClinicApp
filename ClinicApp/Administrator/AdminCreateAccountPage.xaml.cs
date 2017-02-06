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
    /// Interaction logic for AdminCreateAccountPage.xaml
    /// </summary>
    public partial class AdminCreateAccountPage : Page
    {
        private MetroWindow window;
        public AdminCreateAccountPage()
        {
           InitializeComponent();
            btnSave.Click += BtnSave_Click;
            btnGeneratePassword.Click += BtnGeneratePassword_Click;
            Loaded += AdminCreateAccountPage_Loaded;
        }

        private async void AdminCreateAccountPage_Loaded(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this) as MetroWindow;

            try
            {
                comboBoxRole.ItemsSource = UserRepository.GetAllRoles();

            }
            catch (Exception exception)
            {
                    await window.ShowMessageAsync("Something went wrong !", $"{exception.Message}");

            }
        }

        private void BtnGeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            var first = textBoxFirstname?.Text.Substring(0, 3);
            var last = textBoxLastname?.Text.Substring(0, 3);
            var full = first + last;
            textBoxPassword.Text = $"{full}123";
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            

            if (!string.IsNullOrWhiteSpace(textBoxFirstname.Text)
                || !string.IsNullOrWhiteSpace(textBoxLastname.Text)
                || !string.IsNullOrWhiteSpace(textBoxUsername.Text)
                || !string.IsNullOrWhiteSpace(textBoxPassword.Text)
                || !string.IsNullOrWhiteSpace(comboBoxRole.Text)
                )
            {
                try
                {
                    UserRepository.AddUser(new User
                    {
                        FirstName = textBoxFirstname.Text,
                        LastName = textBoxLastname.Text,
                        UserName = textBoxUsername.Text,
                        Password = textBoxPassword.Text,
                        RoleId = (int)comboBoxRole.SelectedValue
                    });
                    await window.ShowMessageAsync("Success !", $"{textBoxFirstname.Text + " " + textBoxLastname.Text} Added Successfully");
                }
                catch (Exception exception)
                {
                    await window.ShowMessageAsync("Something went wrong !", $"{exception.Message}");

                }

            }

           


        }
    }
}
