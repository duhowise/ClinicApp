﻿using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicApp.Logic;
using ClinicModel;
using MahApps.Metro.Controls;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharDispenseDrug.xaml
    /// </summary>
    public partial class PharDispenseDrug : MetroWindow
    {
        BackgroundWorker _remainingDrugsBackgroundWorker=new BackgroundWorker();
        

            CMB cmb = new CMB();
        public PharDispenseDrug()
        {
            InitializeComponent();
            _remainingDrugsBackgroundWorker.WorkerSupportsCancellation = false;
            _remainingDrugsBackgroundWorker.WorkerReportsProgress = true;
            _remainingDrugsBackgroundWorker.WorkerSupportsCancellation = true;
            _remainingDrugsBackgroundWorker.DoWork += _remainingDrugsBackgroundWorker_DoWork;
            _remainingDrugsBackgroundWorker.RunWorkerCompleted += _remainingDrugsBackgroundWorker_RunWorkerCompleted;

            //DataContext = new DrugRepository().DrugAutoComplete();

        }

        private string drugName;

        public string DrugName
        {
            get { return drugName; }
            set {drugName = value;}
        }

        private void _remainingDrugsBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LbRemainingDrugs.Text = e.Result.ToString();
        }

        private void _remainingDrugsBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
               e.Result=DrugRepository.GetRemainingDrugs(new Drug {BrandName = drugName});

        }

        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DispenseProvidedId.Text) || string.IsNullOrEmpty(DispenseDrugName.DisplayMemberPath) || string.IsNullOrEmpty(DispenseDrugQuantity.Text))
            {
                cmb.Message = "All Fields Are Required";
                cmb.Show();
                
            }
            else
            {

                new Pharmacy().DispenseDrug(DispenseProvidedId.Text, DispenseDrugName.SearchText, DispenseDrugQuantity.Text, MainWindow.ID);

                this.DispenseDrugName.SearchText = "";
                this.DispenseDrugQuantity.Text = "";


            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            var response = MessageBox.Show("Do you really want to close this window", "Exit",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.Yes)
            {
                Hide();
                _remainingDrugsBackgroundWorker.CancelAsync();
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DispenseProvidedId.Text = ViewPatient.PatientList1[2];
        }

        private void DispenseDrugName_LostFocus(object sender, RoutedEventArgs e)
        {
            //compute availables here
            if (!string.IsNullOrEmpty(this.DispenseDrugName.SearchText))
            {
                LbRemainingDrugs.Text=DrugRepository.GetRemainingDrugs(new Drug {BrandName = DispenseDrugName.SearchText }).ToString();
            }

        }

        private void DispenseDrugQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num;
            if (!string.IsNullOrEmpty(DispenseDrugQuantity.Text))
            {
                if (DrugRepository.GetRemainingDrugs(new Drug {BrandName = DispenseDrugName.SearchText }) > Convert.ToInt32(DispenseDrugQuantity.Text))
                    LbRemainingDrugs.Text =Convert.ToInt32(DrugRepository.GetRemainingDrugs(new Drug {BrandName = DispenseDrugName.SearchText })) - Convert.ToInt32(DispenseDrugQuantity.Text) + "";
                else
                {
                        cmb.Message = "DrugsOld available is less than \nthe quantity specified";
                        cmb.Show();
                }
                
            }
           
        }

        private void DispenseDrugName_SearchTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DispenseDrugName.SearchText))
            { 
                drugName = DispenseDrugName.SearchText;
                if (!_remainingDrugsBackgroundWorker.IsBusy)
                    _remainingDrugsBackgroundWorker.RunWorkerAsync();
            }
        }
    }
}
