using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharSearchPatient.xaml
    /// </summary>
    public partial class PharSearchPatient : MetroWindow
    {
        private BackgroundWorker patientSearchWorker=new BackgroundWorker();
        List<Patient>patients=new List<Patient>();
        List<Patient> result=new List<Patient>();
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

        private void PharSearchPatient_Closing(object sender, CancelEventArgs e)
        {
            if (patientSearchWorker.IsBusy)
            {
                patientSearchWorker.CancelAsync();
            }
        }

        private void PatientSearchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            patients =(List<Patient>) e.Result;
        }


        private void PatientSearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = new PatientRepository().GetAllPatients();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
           result = patients.FindAll(p => p.ProvidedId.Contains(TbPatientSearch.Text));
            PatientsSearchList.ItemsSource = result;
            //foreach (var items in result)
            //{
            //    PatientsSearchList.Items.Add(items);
            //}

        }

        
    }
}
