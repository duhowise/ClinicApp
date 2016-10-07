using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicApp.Logic;
using ClinicModel;

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

        CMB cmb = new CMB();
        public PharPatientDetailsDispensary()
        {
            InitializeComponent();
            _remainingDrugsBackgroundWorker.WorkerSupportsCancellation = false;
            _remainingDrugsBackgroundWorker.WorkerReportsProgress = true;
            _remainingDrugsBackgroundWorker.WorkerSupportsCancellation = true;
            _remainingDrugsBackgroundWorker.DoWork += _remainingDrugsBackgroundWorker_DoWork;
            _remainingDrugsBackgroundWorker.RunWorkerCompleted += _remainingDrugsBackgroundWorker_RunWorkerCompleted;

            DataContext = new DrugRepository().DrugAutoComplete();
            patient = PharSearchPatient.Patient;
            if (patient!=null)
            {
                consultation = new PatientRepository().PatientHistory(patient);
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
            e.Result = new DrugRepository().GetRemainingDrugs(new Drug { brandName = drugName });

        }

        private void DispenseDrugQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num;
            if (!string.IsNullOrEmpty(DispenseDrugQuantity.Text))
            {
                if (new DrugRepository().GetRemainingDrugs(new Drug { brandName = DispenseDrugName.SearchText }) > Convert.ToInt32(DispenseDrugQuantity.Text))
                    LbRemainingDrugs.Text = Convert.ToInt32(new DrugRepository().GetRemainingDrugs(new Drug { brandName = DispenseDrugName.SearchText })) - Convert.ToInt32(DispenseDrugQuantity.Text) + "";
                else
                {
                    cmb.Message = "DrugsOld available is less than \nthe quantity specified";
                    cmb.Show();
                }

            }
        }

        private void DispenseDrugName_LostFocus(object sender, RoutedEventArgs e)
        {
            //compute availables here
            if (!string.IsNullOrEmpty(this.DispenseDrugName.SearchText))
            {
                LbRemainingDrugs.Text = new DrugRepository().GetRemainingDrugs(new Drug { brandName = DispenseDrugName.SearchText }).ToString();
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
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // TODO save patinent complaint
            if (string.IsNullOrEmpty(DispenseProvidedId.Text) || string.IsNullOrEmpty(DispenseDrugName.DisplayMemberPath) || string.IsNullOrEmpty(DispenseDrugQuantity.Text))
            {
                cmb.Message = "All Fields Are Required";
                cmb.Show();
                //MessageBox.Show("All Feild Are Required", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                new Pharmacy().DispenseDrug(DispenseProvidedId.Text, DispenseDrugName.SearchText, DispenseDrugQuantity.Text, CurrentUserLoggedInData.Id);

                //MessageBox.Show("DrugsOld saved Saved Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DispenseDrugName.SearchText = "";
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
            PatientName.Content = (patient.FirstName + " " + patient.LastName).ToUpper();
            PatientDesignation.Content = $"Designation: {patient.Designation}";
            PatientTemperature.Content = $"{consultation.Temperature} °C";
            PatientBloodPressure.Content = $"Blood pressure : {consultation.BloodPressure}";
            PatientLastVisited.Content = $"Consultation Date: {consultation.Date.ToShortDateString()}";
            tbDiagnosis.Text = $"{consultation.Diagnosis}";
            tbLabFindings.Text = $"{consultation.Symptoms}";
            
            tbPrescription.Text = $"{consultation.Prescription}";
            DispenseProvidedId.Text = patient.ProvidedId;
        }
    }
}
