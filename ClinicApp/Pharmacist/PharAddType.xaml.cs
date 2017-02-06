using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharAddType.xaml
    /// </summary>
    public partial class PharAddType : MetroWindow
    {
      //  private CMB cmb = new CMB();
        public PharAddType()
        {
            InitializeComponent();
            this.Closing += PharAddType_Closing;
        }

        private async void PharAddType_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(typeName.Text))
            {
                var response = await this.ShowMessageAsync("Do you really want to stop Adding new Drug Form\n All your changes will be discarded", "Exit", MessageDialogStyle.AffirmativeAndNegative);
                if (response == MessageDialogResult.Affirmative) { Hide(); } else { e.Cancel = true; }
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void Cancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        private async void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<DrugForm> drugForms = (List<DrugForm>)new DrugRepository().GetDrugForms();
            var result = drugForms.FindAll(s => s.name.Equals(typeName.Name));
            if (!string.IsNullOrWhiteSpace(typeName.Text))
            {
                if (result.Count==0)
                {
                    new DrugRepository().AddNewDrugType(new DrugForm
                    {
                        name = typeName.Text
                    });
                    await this.ShowMessageAsync("Success!",$"Succcessfully Added {typeName.Text}");
                    typeName.Text = "";
                }

            }
        }
        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
