﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;

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

        private void PharAddDosageForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to stop Adding new Category\n All your changes will be discarded", "Exit",
                             MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes) { Hide(); } else { e.Cancel = true; }
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
        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(dosageFormName.Text))
            {
              new DrugRepository().AddNewDrugType(new DrugForm
              {
                  name = dosageFormName.Text
              });  
            }
        }
    }
}
