using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharAddCategory.xaml
    /// </summary>
    public partial class PharAddCategory : MetroWindow
    { CMB Cmb=new CMB();
        public PharAddCategory()
        {
            InitializeComponent();
            this.Closing += PharAddCategory_Closing;
        }

        private void PharAddCategory_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to stop Adding new Category\n All your changes will be discarded", "Exit",
                             MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes) { Hide(); }else{e.Cancel = true;}

        }

        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<DrugCategory> suppliers = (List<DrugCategory>)new DrugRepository().GetAllDrugs();
            var result = suppliers.FindAll(s => s.name.Equals(categoryName.Text));
            if (!string.IsNullOrWhiteSpace(categoryName.Text))
            {
                if (result.Count == 0)
                {
                    new DrugRepository().AddNewDrugCategory(new DrugCategory
                    {
                        name = categoryName.Text.ToUpper()
                    });
                    Cmb.Message = $"Successfully Added new Category {categoryName.Text}";
                    Cmb.ShowDialog();
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
