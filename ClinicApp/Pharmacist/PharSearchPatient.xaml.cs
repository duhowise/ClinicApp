
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using ClinicApp.Data;
using ClinicApp.Doctor;
using ClinicModel;
using MahApps.Metro.Controls;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharSearchPatient.xaml
    /// </summary>
    public partial class PharSearchPatient : MetroWindow
    {
        private BackgroundWorker patientSearchWorker = new BackgroundWorker();
        List<Patient> patients = new List<Patient>();
       public static Patient patient=new Patient();
        public PharSearchPatient()
        {
            InitializeComponent();
            patientSearchWorker.WorkerSupportsCancellation = true;
            patientSearchWorker.DoWork += PatientSearchWorker_DoWork;
            patientSearchWorker.RunWorkerCompleted += PatientSearchWorker_RunWorkerCompleted;
            this.Closing += PharSearchPatient_Closing;
            if (!patientSearchWorker.IsBusy)
            {
                patientSearchWorker.RunWorkerAsync();
            }
        }

        public static Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        private void PharSearchPatient_Closing(object sender, CancelEventArgs e)
        {
            if (patientSearchWorker.IsBusy)
            {
                patientSearchWorker.CancelAsync();
            }
        }

        private void PatientSearchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            patients = (List<Patient>)e.Result;
        }


        private void PatientSearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = new PatientRepository().GetAllPatients();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbPatientSearch.Text))
            {
                if (!patientSearchWorker.IsBusy)
                {
                    patientSearchWorker.RunWorkerAsync();
                }
                PatientsSearchList.ItemsSource = patients.FindAll(p => p.ProvidedId.ToLower().StartsWith
              (TbPatientSearch.Text.ToLower()) || p.FulName().ToLower().StartsWith(TbPatientSearch.Text.ToLower())
              || p.FulNameR().ToLower().StartsWith(TbPatientSearch.Text.ToLower()));

            }
        }

        private  void PatientsSearchList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
             Patient = PatientsSearchList.SelectedItem as Patient;
            new PatientDetailsForm().ShowDialog();
        }

        private void TbPatientSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            
            if (!string.IsNullOrWhiteSpace(TbPatientSearch.Text))
            {
                if (!patientSearchWorker.IsBusy)
                {
                    patientSearchWorker.RunWorkerAsync();
                }
                PatientsSearchList.ItemsSource = patients.FindAll(p => p.ProvidedId.ToLower().StartsWith
              (TbPatientSearch.Text.ToLower()) || p.FulName().ToLower().StartsWith(TbPatientSearch.Text.ToLower())
              || p.FulNameR().ToLower().StartsWith(TbPatientSearch.Text.ToLower()));

            }
        }
    }
}