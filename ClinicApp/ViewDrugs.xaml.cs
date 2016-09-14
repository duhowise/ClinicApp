using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClinicApp.Logic;
using Telerik.Windows.Automation.Peers;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using MessageBox = System.Windows.MessageBox;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for ViewDrugs.xaml
    /// </summary>
    public partial class ViewDrugs : Window, IRefreshDataGridView
{

        

        public static  List<string> newInstance=new List<string>();


        public ViewDrugs()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrugList.ItemsSource = new Drug().RetrieveAll();
            CurrentUserLoggedInData.IsLoaded = true;


        }

        private void DrugList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var item = DrugList.CurrentItem as DataItemCollection; 
            // if (item != null) MessageBox.Show(item[0].ToString());
            var item = DrugList.CurrentCell.Value;

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

        private void RadGridView_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            //var item = DrugList.CurrentCell.Value;            
               
            //if (item is string)
            //{
            //    //MessageBox.Show(item.ToString());
            //   newInstance= Drug.FetchByName(item.ToString());
            //    var updateDrugs=new PharUpdateDrug();
            //    updateDrugs.Show();
            //    updateDrugs.DrugName.Text = newInstance[0];
            //    updateDrugs.BoxQuantity.Text = newInstance[1];
            //    updateDrugs.NumberInBox.Text = newInstance[2];
            //    updateDrugs.TotalQuantity.Text = newInstance[3];

            //}
        }

        public void reloadData()
        {
            //CollectionViewSource.GetDefaultView(DrugList.ItemsSource).Refresh();
          

        }

        private void RadGridView_Sorting(object sender, GridViewSortingEventArgs e)
        {
            //DrugList.ItemsSource = new Drug().RetrieveAll();
        }

        private void RadGridView_Sorted(object sender, GridViewSortedEventArgs e)
        {
            //DrugList.ItemsSource = new Drug().RetrieveAll();
        }
    }
}