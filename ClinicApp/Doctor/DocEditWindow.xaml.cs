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
            DocPatientDetailConsultation.Changed = true;

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
