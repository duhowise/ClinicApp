
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using ClinicApp.Data;
using ClinicApp.Doctor;
using ClinicModel;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;

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
            PatientsSearchList.ItemsSource = patients.FindAll(p => p.ProvidedId.Contains(TbPatientSearch.Text));
            
           

        }

        private async void PatientsSearchList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
             Patient = PatientsSearchList.SelectedItem as Patient;
            //var dispensary=new PharDispenseDrug();
            //  dispensary.DispenseProvidedId.Text = info?.ProvidedId;
            //  dispensary.ShowDialog();


            //if (info != null)
            //{
            //    info.DispensedDrugs = new PatientRepository().PatientDrugHistory(info);
            //    info.Consultations = new PatientRepository().PatientHistory(info);
            //}
            new PatientDetailsForm().ShowDialog();
           
            
            

        }

        private void TbPatientSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!patientSearchWorker.IsBusy)
            {
                patientSearchWorker.RunWorkerAsync();
            }
            PatientsSearchList.ItemsSource = patients.FindAll(p => p.ProvidedId.Contains(TbPatientSearch.Text));

        }
    }
}