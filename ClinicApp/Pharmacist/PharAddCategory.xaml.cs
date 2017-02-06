using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for PharAddCategory.xaml
    /// </summary>
    public partial class PharAddCategory : MetroWindow
    {// CMB Cmb=new CMB();
        public PharAddCategory()
        {
            InitializeComponent();
            this.Closing += PharAddCategory_Closing;
        }

        private async void PharAddCategory_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(categoryName.Text))
            {
                var response = await this.ShowMessageAsync("Cancel", "Do you really want to stop Adding new Category\n All your changes will be discarded", MessageDialogStyle.AffirmativeAndNegative);
                if (response == MessageDialogResult.Affirmative) { Hide(); } else { e.Cancel = true; }

            }

        }

        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private async void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<DrugCategory> suppliers = new DrugRepository().GetDrugCategories().ToList();
            var result = suppliers.FindAll(s => s.name.Equals(categoryName.Text));
            if (!string.IsNullOrWhiteSpace(categoryName.Text))
            {
                if (result.Count == 0)
                {
                    new DrugRepository().AddNewDrugCategory(new DrugCategory
                    {
                        name = categoryName.Text.ToUpper()
                    });
                    await this.ShowMessageAsync("Success",$"Successfully Added new Category {categoryName.Text}");
                    categoryName.Text = "";
                }
            }
        }

        private void Cancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
          Close();
            
        }
    }
}
