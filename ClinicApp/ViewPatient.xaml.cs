using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for ViewPatient.xaml
    /// </summary>
    public partial class ViewPatient : Window
    {
        private string _MenuAction;
        private static List<string> _patientList = new List<string>();
        private CMB cmb;

        public static int cMenu { get; set; }


        // the MenuAction code to get the patient datagrid details
        //when complaint or view patient button is clicked
        public string MenuAction
        {
            get
            {
                return _MenuAction;
            }

            set
            {
                _MenuAction = value;
            }
        }

        public static List<string> PatientList1
        {
            get
            {
                return _patientList;
            }

            set
            {
                _patientList = value;
            }
        }

        public ViewPatient()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //PatientList.ItemsSource = GetPatientData.All();
            PatientList.ItemsSource = new Patient().RetrieveAll();
          
           

        }


        private void RadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            cmb = new CMB();
            var item = PatientList.CurrentCell.Value;
            if (item is string)
            {
                //MessageBox.Show(item.ToString());
                PatientList1 = Patient.FetchPatientById(item.ToString());

                if (PatientList1.Count > 0)
                {
                    if (_MenuAction == "doc")
                    {
                        var c = new DocContextMenu();
                        c.ShowCenteredToMouse();
                    }
                    else if(_MenuAction == "phar")
                    {
                        var p = new PharContextMenu();
                        p.ShowCenteredToMouse();
                    }


                }
                else
                {
                   
                    cmb.Message = "Select Provided Id to view Details";
                    cmb.Show();
                }
            }

        }

       
    }
}
