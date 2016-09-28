using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicApp.Logic;
using ClinicModel;
using Telerik.Windows.Controls;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for ViewDrugs.xaml
    /// </summary>
    public partial class ViewDrugs : Window, IRefreshDataGridView
{

        

        


        public ViewDrugs()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrugList.ItemsSource = new DrugRepository().GetAllDrugs();
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
               var result= new DrugRepository().GetDrugByName(item.ToString());
                var updateDrugs = new Pharmacist.PharUpdateDrug();
                var cmd = new CMB();
                cmd.Message = "";
                updateDrugs.Show();
                updateDrugs.DrugName.Text = result.brandName;

                Pharmacy.OldBoxValue = Convert.ToInt32(result.NumberPackInBox);
                Pharmacy.OldNumberInBoxValue = Convert.ToInt32(result.NumberPackInBox);
                Pharmacy.OldTotalQunatityValue = Convert.ToInt32(result.Quantity);

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
            //   newInstance= DrugsOld.FetchByBrandName(item.ToString());
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
            //DrugList.ItemsSource = new DrugsOld().RetrieveAll();
        }

        private void RadGridView_Sorted(object sender, GridViewSortedEventArgs e)
        {
            //DrugList.ItemsSource = new DrugsOld().RetrieveAll();
        }
    }
}