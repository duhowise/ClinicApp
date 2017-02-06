using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharPatientDetailsDispensary.xaml
    /// </summary>
    public partial class PharPatientDetailsDispensary : UserControl
    {
        BackgroundWorker _remainingDrugsBackgroundWorker = new BackgroundWorker();
        static Patient patient = new Patient();
        Consultation _consultation = new Consultation();
        Drug drug = new Drug();
        List<string> drugsordate = new List<string>();
        //CMB cmb = new CMB();
       

        public PharPatientDetailsDispensary()
        {
            InitializeComponent();
            DispenseDrugName.FilterMode = AutoCompleteFilterMode.StartsWithOrdinal;
            DispenseDrugName.ItemsSource = new DrugRepository().DrugAutoComplete();
            PatientHistory.SelectionChanged += PatientHistory_SelectionChanged;
            _remainingDrugsBackgroundWorker.WorkerSupportsCancellation = false;
            _remainingDrugsBackgroundWorker.WorkerReportsProgress = true;
            _remainingDrugsBackgroundWorker.WorkerSupportsCancellation = true;
            _remainingDrugsBackgroundWorker.DoWork += _remainingDrugsBackgroundWorker_DoWork;
            _remainingDrugsBackgroundWorker.RunWorkerCompleted += _remainingDrugsBackgroundWorker_RunWorkerCompleted;
            DispenseDrugName.SelectionChanged += DispenseDrugName_SelectionChanged;
           
            GetDispensedDrugs();
        }

        private void GetDispensedDrugs()
        {
            patient = PharSearchPatient.Patient;

            List<DispensedDrug> dispensed;
            List<Drug> alldrugs;
            dispensed = (List<DispensedDrug>)new PatientRepository().PatientDrugHistory(patient);
            alldrugs = (List<Drug>)new DrugRepository().GetAllDrugs();
            if (patient != null)
            {
                _consultation = new PatientRepository().PatientHistory(patient);
                PatientHistory.ItemsSource = new PatientRepository().AllPatientHistory(patient);
            }
            foreach (var pill in dispensed.FindAll(d => d.ConsultationId == _consultation.Id))
            {
                drugsordate.Add(alldrugs.Find(d => d.Id == pill.DrugId).BrandName);
            }
            tbDrugsDispensed.Text = string.Join(" , ", drugsordate.ToList());


        }

        private void PatientHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DateTime dateselect =Convert.ToDateTime(PatientHistory.SelectedItem.ToString());
           var dateselect =PatientHistory.SelectedItem as Consultation;
            List<Consultation> consultation =
                 new PatientRepository().AllPatientHistory(patient) as List<Consultation>;
            if (consultation != null)
            {
                _consultation = null;
                _consultation = consultation.Find(p => p.Date.Date.Equals(dateselect?.Date.Date));
            }
            PharPatientDetailsDispensary_OnLoaded(sender, e);
            //GetDispensedDrugs();
        }

        private void DispenseDrugName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DrugName= DispenseDrugName.SelectedItem.ToString();
            //if (!string.IsNullOrWhiteSpace(DrugName))
            //{
            //  drug = new DrugRepository().GetDrugByName(drugName);
            //    if (!_remainingDrugsBackgroundWorker.IsBusy)
            //        _remainingDrugsBackgroundWorker.RunWorkerAsync();
            //}
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
            e.Result = DrugRepository.GetRemainingDrugs(new Drug {BrandName = drugName});


        }

        public void GetTotalNumber()
        {

            LbRemainingDrugs.Text =
                Convert.ToInt32(DrugRepository.GetRemainingDrugs(new Drug {BrandName = drugName})) -
                Convert.ToInt32(DispenseDrugQuantity.Text) + "";
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
                    //cmb.Message = "DrugsOld available is less than \nthe quantity specified";
                   // cmb.Show();
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
            var metrowindow = Window.GetWindow(this) as MetroWindow;
            if (string.IsNullOrWhiteSpace(DispenseProvidedId.Text) || string.IsNullOrWhiteSpace(DispenseDrugName.Text) ||
                string.IsNullOrWhiteSpace(DispenseDrugQuantity.Text))
            {
               await metrowindow.ShowMessageAsync( "Attention!", "All Fields Are Required");

            }
            else
            {

                new DrugRepository().DispenseDrug(new DispensedDrug
                {
                    DrugId =Convert.ToInt32(drug.Id),
                    PatientId =Convert.ToInt32(patient.Id),
                    Quantity = Convert.ToInt32(DispenseDrugQuantity.Text),
                    ConsultationId = _consultation.Id,
                    UserId = MainWindow.ID
                });
                tbDrugsDispensed.Text += $" {drug.BrandName};";
               await metrowindow.ShowMessageAsync("",$"{drug.BrandName} dispensed to {patient.FirstName + " " + patient.LastName}");
                this.DispenseDrugName.Text = "";
                this.DispenseDrugQuantity.Text = "";
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
            if (_consultation?.IsSensitive == 0)
            {
                tbDiagnosis.Text = $"{_consultation?.Diagnosis}";
            }
            else
            {
                tbDiagnosis.Text = @"
                                     *****************************
                                     *****************************";
            }
            PatientName.Content = patient.FulName().ToUpper();
            PatientDesignation.Content = $"Designation: {patient.Designation}";
            PatientTemperature.Content = $"Temperature :{_consultation?.Temperature} °C";
            PatientPulse.Content = $"Pulse :{_consultation?.Pulse} bpm";
            PatientWeight.Content = $"Weight :{_consultation?.Weight} kg";
            PatientBloodPressure.Content = $"Blood pressure : {_consultation?.BloodPressure}";
            PatientLastVisited.Content = $"Consultation Date: {_consultation?.Date.ToShortDateString()}";
            tbLabFindings.Text = $"{_consultation?.Symptoms}";
            tbPrescription.Text = $"{_consultation?.Prescription}";
            DispenseProvidedId.Text = patient.ProvidedId;


        }

        private void DispenseDrugName_TextChanged(object sender, RoutedEventArgs e)
        {
            DrugName = DispenseDrugName.Text;
            if (!string.IsNullOrWhiteSpace(DrugName))
            {
                drug = new DrugRepository().GetDrugByName(drugName);
                if (!_remainingDrugsBackgroundWorker.IsBusy)
                    _remainingDrugsBackgroundWorker.RunWorkerAsync();
            }
        }

        //private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (DatePicker.SelectedDate != null)
        //    {
        //        DateTime dateselect = DatePicker.SelectedDate.Value;
        //       List<Consultation> consultation =
        //            new PatientRepository().AllPatientHistory(patient) as List<Consultation>;
        //        if (consultation != null)
        //        {
        //            _consultation = null;
        //            _consultation = consultation.Find(p => p.Date.Date.Equals(dateselect));
        //        }
        //        PharPatientDetailsDispensary_OnLoaded(sender, e);
        //    }
        //}


    }
}
