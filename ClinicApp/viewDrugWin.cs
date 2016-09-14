using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicApp.Logic;

namespace ClinicApp
{
    public partial class viewDrugWin : Form
    {
        public static List<string> newInstance = new List<string>();
        public viewDrugWin()
        {
            InitializeComponent();
            
        }

        private void viewDrugWin_Load(object sender, EventArgs e)
        {
            DrugView.DataSource = new Drug().RetrieveAll();
        }

        private void DrugView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //var item = DrugView.CurrentCell.Value;

            //if (item is string)
            //{
            //    //MessageBox.Show(item.ToString());
            //    newInstance = Drug.FetchByName(item.ToString());
            //    var updateDrugs = new PharUpdateDrug();
            //    var cmd = new CMB();
            //    cmd.Message = "";
            //    updateDrugs.Show();
            //    updateDrugs.DrugName.Text = newInstance[0];

            //    Pharmacy.OldBoxValue = Convert.ToInt32(newInstance[1]);
            //    Pharmacy.OldNumberInBoxValue = Convert.ToInt32(newInstance[2]);
            //    Pharmacy.OldTotalQunatityValue = Convert.ToInt32(newInstance[3]);

            //    //updateDrugs.BoxQuantity.Text = newInstance[1];
            //    //updateDrugs.NumberInBox.Text = newInstance[2];
            //    //updateDrugs.TotalQuantity.Text = newInstance[3];

            //}
        }

        private void DrugView_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var item = DrugView.CurrentCell.Value;

            if (item is string)
            {
                //MessageBox.Show(item.ToString());
                newInstance = Drug.FetchByName(item.ToString());
                var updateDrugs = new PharUpdateDrug();
                var cmd = new CMB();
                cmd.Message = "";
                updateDrugs.Show();
                updateDrugs.DrugName.Text = newInstance[0];

                Pharmacy.OldBoxValue = Convert.ToInt32(newInstance[1]);
                Pharmacy.OldNumberInBoxValue = Convert.ToInt32(newInstance[2]);
                Pharmacy.OldTotalQunatityValue = Convert.ToInt32(newInstance[3]);

                //updateDrugs.BoxQuantity.Text = newInstance[1];
                //updateDrugs.NumberInBox.Text = newInstance[2];
                //updateDrugs.TotalQuantity.Text = newInstance[3];

            }

        }
    }
}
