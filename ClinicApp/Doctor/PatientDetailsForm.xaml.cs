using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClinicApp.Data;
using ClinicApp.Logic;
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
        List<Patient> patients = new List<Patient>();
        List<Patient> result = new List<Patient>();
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
            patients = (List<Patient>)e.Result;
        }


        private void PatientSearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = new PatientRepository().GetAllPatients();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            result = patients.FindAll(p => p.ProvidedId.Contains(TbPatientSearch.Text));
            PatientsSearchList.ItemsSource = result;
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
            navigator.NavigateToPharPatientDetailDispensaryControl();
        }
    }
}
