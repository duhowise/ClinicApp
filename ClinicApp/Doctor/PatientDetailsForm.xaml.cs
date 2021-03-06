﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ClinicApp.Data;
using ClinicApp.Logic;
using ClinicApp.Nurse;
using ClinicModel;
using MahApps.Metro.Controls;

namespace ClinicApp.Doctor
{
    /// <summary>
    /// Interaction logic for PatientDetailsForm.xaml
    /// </summary>
    public partial class PatientDetailsForm : MetroWindow
    {
        private BackgroundWorker patientSearchWorker = new BackgroundWorker();
        private MainContentView navigator;
        List<Patient> _patients = new List<Patient>();
        List<Patient> _result = new List<Patient>();
        public PatientDetailsForm()
        {
            InitializeComponent();
            patientSearchWorker.WorkerSupportsCancellation = true;
            patientSearchWorker.DoWork += PatientSearchWorker_DoWork;
            patientSearchWorker.RunWorkerCompleted += PatientSearchWorker_RunWorkerCompleted;
            this.Closing += PatientDetailsForm_Closing;
            if (!patientSearchWorker.IsBusy)
            {
                patientSearchWorker.RunWorkerAsync();
            }
            
        }


        private void PatientSearchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _patients = (List<Patient>)e.Result;
        }


        private void PatientSearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = new PatientRepository().GetAllPatients();
        }


        private void PatientDetailsForm_Closing(object sender, CancelEventArgs e)
        {
            if (patientSearchWorker.IsBusy)
            {
                patientSearchWorker.CancelAsync();
            }
        }

        private void NavigateToView(UserControl view)
        {
            MainArea.Content = view;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MainContentView(NavigateToView);
            navigator = new MainContentView(NavigateToView);
            if (MainWindow.ID == 3)
            {navigator.NavigateToPharPatientDetailDispensaryControl();
            }
            else if (MainWindow.ID == 1)
            {
                navigator.NavigateToDocPatientDetailConsultationControl(); 
            } else if (MainWindow.ID == 2)
            {
                new NurAddPatient().ShowDialog();
            }

        }
    }
}
