using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharAddType.xaml
    /// </summary>
    public partial class PharAddType : MetroWindow
    {
        private CMB cmb = new CMB();
        public PharAddType()
        {
            InitializeComponent();
            this.Closing += PharAddType_Closing;
        }

        private void PharAddType_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to stop Adding new Drug Form\n All your changes will be discarded", "Exit",
                             MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes) { Hide(); } else { e.Cancel = true; }
        }

        private void Cancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(typeName.Text))
            {
              new DrugRepository().AddNewDrugType(new DrugForm
              {
                  name = typeName.Text
              });
                cmb.Message = $"Succcessfully Added {typeName.Text}";
                cmb.Show();

            }
        }
        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
