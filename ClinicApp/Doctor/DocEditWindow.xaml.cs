using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;

namespace ClinicApp.Doctor
{
    /// <summary>
    /// Interaction logic for DocEditWindow.xaml
    /// </summary>
    public partial class DocEditWindow : MetroWindow
    {
        private string _editSource = string.Empty;
        Consultation _consultation=new Consultation();
        public DocEditWindow(string owner,Consultation consultation)
        {
            InitializeComponent();
            _consultation = consultation;
            heading.Content = $"Edit - {owner}";
            _editSource = owner;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
           new PatientRepository().UpdateConsultation(_consultation);
            OnEditFinished();
        }
        //declare delegate
        public delegate void EditFinishedEventHandler(object source, EventArgs args);
        //define event based on edit finished delegate
        public event EditFinishedEventHandler EditFinished;
        //raise finished edit delegate
        protected virtual void OnEditFinished()
        {
            if (EditFinished != null)
                EditFinished(this,EventArgs.Empty);

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_editSource == "Diagnosis")
            {
              tbDocEdit.Text = _consultation.Diagnosis ;
            }
            else if (_editSource == "Findings")
            {
              tbDocEdit.Text = _consultation.Investigation ;

            }
            else if (_editSource == "Dispensary")
            {
                 tbDocEdit.Text=_consultation.Prescription;

            }
            else if (_editSource == "Prescriptions")
            {
                 tbDocEdit.Text= _consultation.Prescription;


            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
           new PatientRepository().UpdateConsultation(_consultation);
            Close();
        }

        private void tbDocEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_editSource == "Diagnosis")
            {
                _consultation.Diagnosis = tbDocEdit.Text;
            }
            else if (_editSource == "Findings")
            {
                _consultation.Investigation = tbDocEdit.Text;

            }
            else if (_editSource == "Dispensary")
            {
                _consultation.Prescription = tbDocEdit.Text;

            }
            else if (_editSource == "Prescriptions")
            {
                _consultation.Prescription = tbDocEdit.Text;


            }
        }
    }
}
