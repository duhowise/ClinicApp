using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClinicApp.Data;
using ClinicApp.Logic;
using Telerik.WinControls.UI;

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
            DrugView.DataSource = new DrugRepository().GetAllDrugs();
        }

        private void DrugView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //var item = DrugView.CurrentCell.Value;

            //if (item is string)
            //{
            //    //MessageBox.Show(item.ToString());
            //    newInstance = DrugsOld.FetchByBrandName(item.ToString());
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

        private void DrugView_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            var item = DrugView.CurrentCell.Value;

            if (item is string)
            {
                //MessageBox.Show(item.ToString());
                newInstance = (List<string>) new DrugRepository().FetchByBrandName(item.ToString());
                var updateDrugs = new Pharmacist.PharSearchDrug();
                var cmd = new CMB();
                cmd.Message = "";
                //updateDrugs.Show();
                //updateDrugs.DrugName.Text = newInstance[0];

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
