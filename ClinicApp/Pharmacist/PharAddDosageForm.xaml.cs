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
    /// Interaction logic for PharAddDosageForm.xaml
    /// </summary>
    public partial class PharAddDosageForm : MetroWindow
    {
        public PharAddDosageForm()
        {
            InitializeComponent();
            this.Closing += PharAddDosageForm_Closing;
        }

        private async void PharAddDosageForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(dosageFormName.Text))
            {
                var response = await this.ShowMessageAsync("Do you really want to stop Adding new Category\n All your changes will be discarded", "Exit", MessageDialogStyle.AffirmativeAndNegative);
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
        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private async void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<DrugDosageForm> suppliers = (List<DrugDosageForm>)new DrugRepository().GetDosageForms();
            var result = suppliers.FindAll(s => s.name.Equals(dosageFormName.Text));
            if (!string.IsNullOrWhiteSpace(dosageFormName.Text))
            {
                if (result.Count==0)
                {
                    new DrugRepository().AddNewDrugDosage(new DrugDosageForm
                    {
                        name = dosageFormName.Text
                    });
                    await this.ShowMessageAsync("Success!",$"Successfully Added new Dosage Form {dosageFormName.Text}");
                    dosageFormName.Text = "";
                    }
            }
        }
    }
}
