using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClinicApp.Data;
using ClinicApp.Doctor;
using ClinicApp.Pharmacist;
using ClinicModel;
using Telerik.WinControls.UI;

namespace ClinicApp
{
    public partial class viewPatientWin : Form
    {
        private string _MenuAction;
        private static List<string> _patientList = new List<string>();
        private CMB cmb;

        public static int cMenu { get; set; }

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
        public viewPatientWin()
        {
            InitializeComponent();
        }

        private void viewPatientWin_Load(object sender, EventArgs e)
        {
            PatientView.DataSource = new PatientRepository().GetAllPatients();
        }

        private void viewPatientWin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
        }

        private void PatientView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void PatientView_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            cmb = new CMB();
            var item = PatientView.CurrentCell.Value;
            if (item is string)
            {
                //MessageBox.Show(item.ToString());
                //PatientList1 = Patient.FetchPatientById(item.ToString());

                if (PatientList1.Count > 0)
                {
                    if (_MenuAction == "doc")
                    {
                        var c = new DocContextMenu();
                        c.ShowCenteredToMouse();
                    }
                    else if (_MenuAction == "phar")
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
