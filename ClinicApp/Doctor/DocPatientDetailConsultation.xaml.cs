using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ClinicApp.Data;
using ClinicApp.Logic;
using ClinicApp.Pharmacist;
using ClinicModel;

namespace ClinicApp.Doctor
{
    /// <summary>
    /// Interaction logic for DocPatientDetailConsultation.xaml
    /// </summary>
    public partial class DocPatientDetailConsultation : UserControl
    {
        static Patient patient = new Patient();
        Consultation _consultation = new Consultation();
        List<string> drugsordate = new List<string>();
        public DocPatientDetailConsultation()
        {
            InitializeComponent();
            patient = PharSearchPatient.patient;
            _consultation = new PatientRepository().PatientHistory(patient);
            //get all consultation history for patient
            PatientHistory.ItemsSource = new PatientRepository().AllPatientHistory(patient);
            if (patient.Designation== "Dependant")
            {
                OtherInfo.Content = $"Card Owner / Guardian: {PatientRepository.GetGuardian(patient).FulName()} Designation: {PatientRepository.GetGuardian(patient).Designation}";
            }

        }
        private void Card_Loaded(object sender, RoutedEventArgs e)
        {
               tbDrugsDispensed.Text = "";
                //get all drugs in the system
                var alldrugs = (List<Drug>)new DrugRepository().GetAllDrugs();
                PatientName.Content = (patient.FulName()).ToUpper();
                PatientDesignation.Content = $"Designation: {patient.Designation}";
                PatientTemperature.Content = $"Temperature :{_consultation?.Temperature} °C";
                PatientPulse.Content = $"Pulse :{_consultation?.Pulse} bpm";
                tbDiagnosis.Text = _consultation?.Diagnosis;
                tbPrescriptions.Text = _consultation?.Prescription;
                tbFindings.Text = _consultation?.Signs;
                PatientWeight.Content = $"Weight :{_consultation?.Weight} kg";
                PatientBloodPressure.Content = $"Blood pressure : {_consultation?.BloodPressure}";
                PatientLastVisited.Content = $"Consultation Date: {_consultation?.Date.ToShortDateString()}";
                //get dispensed drugs for this patient
                List<DispensedDrug> dispensed = (List<DispensedDrug>)new PatientRepository().PatientDrugHistory(patient);

                /*compare the drug ids obtained from the 
                 * dispensed drugs to get names of dispensed drugs*/
                foreach (var pill in dispensed.FindAll(d => d.ConsultationId == _consultation.Id))
                {
                    drugsordate.Add(alldrugs.Find(d => d.Id == pill.DrugId).BrandName);
                }
                foreach (var names in drugsordate)
                {
                    tbDrugsDispensed.Text = tbDrugsDispensed.Text + names + " ,";
                }
                Check();
           
        }
        private void Check()
        {
            if (_consultation!=null)
            {
                if (_consultation.IsSensitive == 1)
                {
                    Status.IsChecked = true;
                }
                else if (_consultation.IsSensitive == 0)
                {
                    Status.IsChecked = false;


                }
                else
                {
                    Status.IsChecked = false;
                }
            }
        }
        public void OnEditFinished(object source, EventArgs args)
        {
            tbDrugsDispensed.Text = "";
            _consultation = null;
            if (PatientHistory.SelectedItem != null)
            {
                _consultation = PatientHistory.SelectedItem as Consultation;

            }
            else
            {
            _consultation = new PatientRepository().PatientHistory(patient);

            }
            Check();
            Card_Loaded(source, null);
        }
        private void PatientHistory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbDrugsDispensed.Text = "";
            _consultation = null;
            _consultation = PatientHistory.SelectedItem as Consultation;
            Check();
            Card_Loaded(sender, e);
        }
        private void btnDocEdit_Click(object sender, RoutedEventArgs e)
        {
           var edit= new DocEditWindow("Diagnosis", _consultation);
            edit.EditFinished += OnEditFinished;
            edit.ShowDialog();
        }
        private void Status_Checked(object sender, RoutedEventArgs e)
        {
            if (_consultation.IsSensitive == 0){_consultation.IsSensitive = 1;}
            Check();
            new PatientRepository().UpdateConsultation(_consultation);

        }
        private void Status_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_consultation.IsSensitive == 1){_consultation.IsSensitive = 0;}
            Check();
           new PatientRepository().UpdateConsultation(_consultation);

        }
        private void FindingsEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var edit=new DocEditWindow("Findings",_consultation);
            edit.EditFinished += OnEditFinished;
            edit.ShowDialog();
        }
        private void PrescEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var edit = new DocEditWindow("Prescriptions", _consultation);
            edit.EditFinished += OnEditFinished;
            edit.ShowDialog();


        }

       
    }
}
