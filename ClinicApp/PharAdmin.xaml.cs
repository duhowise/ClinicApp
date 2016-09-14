﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for PharAdmin.xaml
    /// </summary>
    public partial class PharAdmin : Window
    {
       
        //PharAddSupplier ps = new PharAddSupplier();
        //PharAddDrug pd = new PharAddDrug();
       Drug drug = new Drug();
        

        public PharAdmin()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            new PharAddDrug().ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int a, b;
            ViewPatient.cMenu = 0;
            LoginUserName.Text = CurrentUserLoggedInData.FirstName + " " + CurrentUserLoggedInData.LastName;
            lbTotalDrugs.Content = a = drug.TotalDrugsInStock("Drugs");
            lbDispensedDrugs.Content = b = drug.TotalDrugsInStock("PrescribedDrugs");
            lbAvailableDrugs.Content = a - b;


            frequentdataGrid.ItemsSource = Drug.LoadFrequentDrugs().DefaultView;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //ViewDrugs viewDrug = new ViewDrugs();
            //viewDrug.ShowDialog();
            var viewDrug=new viewDrugWin();
            viewDrug.ShowDialog();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
                new PharAddSupplier().ShowDialog();
           
        }

        private void Dispense_Click(object sender, RoutedEventArgs e)
        {
           
            //ViewPatient viewPatient = new ViewPatient { MenuAction = "phar" };
            //viewPatient.ShowDialog();

            var ViewPatient=new viewPatientWin() { MenuAction = "phar" };
            ViewPatient.ShowDialog();

        }

        private void Logout_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //TODO Logout
            LogUserOut();
        }

        private void LogUserOut()
        {

            var response = System.Windows.MessageBox.Show("Do you really want to Logout", "Exit",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {

            }
            else
            {

                CurrentUserLoggedInData.ClearUserData();
                new MainWindow().Show();
                Hide();
            }
        }

        //private void DrugHistory_Click(object sender, RoutedEventArgs e)
        //{
        //    new PharViewDrugHistory().ShowDialog();
        //}


        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            int a, b;
            lbTotalDrugs.Content = a = drug.TotalDrugsInStock("Drugs");
            lbDispensedDrugs.Content = b = drug.TotalDrugsInStock("PrescribedDrugs");
            lbAvailableDrugs.Content = a - b;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var response = System.Windows.MessageBox.Show("Please be sure to Logout first", "Exit",
               MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                CurrentUserLoggedInData.ClearUserData();
                new MainWindow().Show();
                Hide();
            }
        }

        private void reporting_Click(object sender, RoutedEventArgs e)
        {
            stockTakingReportOne repotOne=new stockTakingReportOne();
            repotOne.Show();
        }
    }
}
