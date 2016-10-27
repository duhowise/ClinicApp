using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicApp.Logic;
using ClinicModel;
using MahApps.Metro.Controls;

namespace ClinicApp.Pharmacist
{

    /// <summary>
    /// Interaction logic for PharUpdateDrug.xaml
    /// </summary>
    public partial class PharSearchDrug : MetroWindow
    {
        private BackgroundWorker _drugSearchWorker = new BackgroundWorker();
        List<Drug> drugs = new List<Drug>();
        public static Drug drug = new Drug();

        CMB cmb = new CMB();
        //public object DrugList { get; private set; }

        public PharSearchDrug()
        {
            InitializeComponent();
            _drugSearchWorker.DoWork += _drugSearchWorker_DoWork;
            _drugSearchWorker.WorkerSupportsCancellation = true;
            _drugSearchWorker.RunWorkerCompleted += _drugSearchWorker_RunWorkerCompleted;
            if (!_drugSearchWorker.IsBusy)
            {
                _drugSearchWorker.RunWorkerAsync();
            }

        }

        private void _drugSearchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            drugs = (List<Drug>)e.Result;
        }

        private void _drugSearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = new DrugRepository().GetAllDrugs();
        }

        private void DrugsSearchList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            drug = DrugsSearchList.SelectedItem as Drug;
            new PharAddDrug(drug).ShowDialog();
        }

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(TbDrugSearch.Text))
            {
                if (!_drugSearchWorker.IsBusy)
                {
                    _drugSearchWorker.RunWorkerAsync();
                }
                DrugsSearchList.ItemsSource = drugs.FindAll(d=>d.BrandName.ToLower()
                .StartsWith(TbDrugSearch.Text.ToLower())||
                d.GenericName.ToLower().StartsWith(TbDrugSearch.Text.ToLower()));


            }
        }

        private void TbDrugSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(TbDrugSearch.Text))
            {
                if (!_drugSearchWorker.IsBusy)
                {
                    _drugSearchWorker.RunWorkerAsync();
                }
                DrugsSearchList.ItemsSource = drugs.FindAll(d => d.BrandName.ToLower()
                .StartsWith(TbDrugSearch.Text.ToLower()) ||
                d.GenericName.ToLower().StartsWith(TbDrugSearch.Text.ToLower()));

            }
        }
    }

    
}
