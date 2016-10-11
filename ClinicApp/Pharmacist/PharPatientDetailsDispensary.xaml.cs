﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicApp.Logic;
using ClinicModel;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharPatientDetailsDispensary.xaml
    /// </summary>
    public partial class PharPatientDetailsDispensary : UserControl
    {
        BackgroundWorker _remainingDrugsBackgroundWorker = new BackgroundWorker();
       static Patient patient=new Patient();
        Consultation consultation=new Consultation();
        Drug  drug=new Drug();

        CMB cmb = new CMB();
        public PharPatientDetailsDispensary()
        {
            InitializeComponent();
            DispenseDrugName.FilterMode = AutoCompleteFilterMode.StartsWithOrdinal;
            DispenseDrugName.ItemsSource = new DrugRepository().DrugAutoComplete();
            _remainingDrugsBackgroundWorker.WorkerSupportsCancellation = false;
            _remainingDrugsBackgroundWorker.WorkerReportsProgress = true;
            _remainingDrugsBackgroundWorker.WorkerSupportsCancellation = true;
            _remainingDrugsBackgroundWorker.DoWork += _remainingDrugsBackgroundWorker_DoWork;
            _remainingDrugsBackgroundWorker.RunWorkerCompleted += _remainingDrugsBackgroundWorker_RunWorkerCompleted;
            DispenseDrugName.SelectionChanged += DispenseDrugName_SelectionChanged;
            patient = PharSearchPatient.Patient;
            if (patient!=null)
            {
                consultation = new PatientRepository().PatientHistory(patient);
            }
        }

        private void DispenseDrugName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DispenseDrugName.Text))
            {
                drugName = DispenseDrugName.SelectedItem.ToString();
                drug=new DrugRepository().GetDrugByName(drugName);
                if (!_remainingDrugsBackgroundWorker.IsBusy)
                    _remainingDrugsBackgroundWorker.RunWorkerAsync();
            }
        }

        private string drugName;

        public string DrugName
        {
            get { return drugName; }
            set { drugName = value; }

        }

        private void _remainingDrugsBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LbRemainingDrugs.Text = e.Result.ToString();
        }

        private void _remainingDrugsBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = new DrugRepository().GetRemainingDrugs(new Drug { BrandName = drugName });
           

        }
        public void GetTotalNumber()
        {
            
                LbRemainingDrugs.Text = Convert.ToInt32(new DrugRepository().GetRemainingDrugs(new Drug { BrandName =drugName })) - Convert.ToInt32(DispenseDrugQuantity.Text) + "";
        }

        private void DispenseDrugQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int num;
            if (!string.IsNullOrEmpty(DispenseDrugQuantity.Text))
            {
                if (Convert.ToInt32(LbRemainingDrugs.Text) > Convert.ToInt32(DispenseDrugQuantity.Text))
                {
                    GetTotalNumber();
                }
                else
                {
                    cmb.Message = "DrugsOld available is less than \nthe quantity specified";
                    cmb.Show();
                }
            }
            
            
        }

       private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to close this window", "Exit",
              MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.Yes)
            {
              
                _remainingDrugsBackgroundWorker.CancelAsync();
                Util.Clear(this);
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            // TODO save patinent complaint
            if (string.IsNullOrEmpty(DispenseProvidedId.Text) || string.IsNullOrEmpty(DispenseDrugName.Text) || string.IsNullOrEmpty(DispenseDrugQuantity.Text))
            {
                cmb.Message = "All Fields Are Required";
                cmb.Show();
            }
            else
            {
                
                new DrugRepository().DispenseDrug(new DispensedDrug
                {
                  DrugId = drug.Id,
                  PatientId = patient.Id,
                  Quantity = Convert.ToInt32(DispenseDrugQuantity.Text),
                  UserId = MainWindow.ID
                });

                cmb.Message = $"{drug.BrandName} dispensed to {patient.FirstName+" "+patient.LastName}";
                cmb.Show();
                this.DispenseDrugName.Text = "";
                this.DispenseDrugQuantity.Text = "";


            }
        }

        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
     

        private void PharPatientDetailsDispensary_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (consultation.IsSensitive == 0) { tbDiagnosis.Text = $"{consultation.Diagnosis}"; }
            else
            {
                tbDiagnosis.Text = @"
                                     *****************************
                                     *****************************
                                     *****************************
                                     *****************************";
            }
            PatientName.Content = (patient.FirstName + " " + patient.LastName).ToUpper();
            PatientDesignation.Content = $"Designation: {patient.Designation}";
            PatientTemperature.Content = $"{consultation.Temperature} °C";
            PatientBloodPressure.Content = $"Blood pressure : {consultation.BloodPressure}";
            PatientLastVisited.Content = $"Consultation Date: {consultation.Date.ToShortDateString()}";
            
            tbLabFindings.Text = $"{consultation.Symptoms}";
            PatientBloodPressure.Content = $"Blood Pressure: {consultation.BloodPressure}";
            tbPrescription.Text = $"{consultation.Prescription}";
            DispenseProvidedId.Text = patient.ProvidedId;
            PatientPulse.Content = $"Pulse :{consultation.Pulse} bpm";
            PatientWeight.Content = $"Weight :{consultation.Weight} kg";

        }
    }
 

}
