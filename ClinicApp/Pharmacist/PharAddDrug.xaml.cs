using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClinicApp.Data;
using ClinicModel;
using MahApps.Metro.Controls;
using MessageBox = System.Windows.MessageBox;
namespace ClinicApp.Pharmacist
{
    /// <summary>
    /// Interaction logic for PharAddDrug.xaml
    /// </summary>
    public partial class PharAddDrug : MetroWindow
    { 
        CMB cmb =new CMB();
        private int boxnumber = 1;
        private int Packnumber = 1;
        private int NumberInPack = 1;
        BackgroundWorker preliminaryBackgroundWorker=new BackgroundWorker();
        public PharAddDrug()
        {
            InitializeComponent();
            this.Closing += PharAddDrug_Closing;
            preliminaryBackgroundWorker.WorkerSupportsCancellation = false;
            preliminaryBackgroundWorker.WorkerReportsProgress = false;
            preliminaryBackgroundWorker.DoWork += PreliminaryBackgroundWorker_DoWork;
            preliminaryBackgroundWorker.RunWorkerCompleted += PreliminaryBackgroundWorker_RunWorkerCompleted; ;
            
            if (!preliminaryBackgroundWorker.IsBusy)
            {
                preliminaryBackgroundWorker.RunWorkerAsync();
            }


        }
        private void PreliminaryBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DrugRepository result =(DrugRepository) e.Result;
            cbSupplier.ItemsSource = new SupplierRepository().GetAllSuppliers();
            cbDosageForm.ItemsSource = result.GetDosageForms();
            cbDrugCategory.ItemsSource = result.GetDrugCategories();
            cbDrugType.ItemsSource = result.GetDrugForms();
            cbPackaging.ItemsSource = new DrugRepository().GetAllDrugPackaging();

        }

        private void PreliminaryBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
           e.Result=new DrugRepository(); 

        }
        public void GetTotalNumber()
        {
           tbTotalQuantity.Text= (boxnumber * Packnumber * NumberInPack).ToString();
        }
        private void PharAddDrug_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to stop Adding new drug\n All your changes will be discarded", "Exit",
                 MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes) { Hide(); } else { e.Cancel = true; }
        }
        
        private void TextValidation(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private  void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void AddNewSupplier_Click(object sender, RoutedEventArgs e)
        {
            PharAddSupplier ps= new PharAddSupplier();

            ps.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbGenericName.Focus();
            cbSupplier.Items.Clear();
            }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbGenericName.Text) ||
                string.IsNullOrWhiteSpace(tbBrandName.Text)
                || string.IsNullOrWhiteSpace(tbNumberInBox.Text) ||
                string.IsNullOrWhiteSpace(cbSupplier.SelectedItem.ToString()) ||
                string.IsNullOrWhiteSpace(tbNumberInPack.Text) ||
                string.IsNullOrWhiteSpace(tbExpiringDate.Text)
                || string.IsNullOrWhiteSpace(tbBox.Text)
                || string.IsNullOrWhiteSpace(cbDosageForm.SelectedItem.ToString())
                || string.IsNullOrWhiteSpace(cbDrugType.SelectedItem.ToString())
                || string.IsNullOrWhiteSpace(cbDrugCategory.SelectedItem.ToString())
                || string.IsNullOrWhiteSpace(cbSupplier.SelectedItem.ToString()))
            {
                cmb.Message = "All Fields Are Required";
                cmb.Show();
            }
            else
            {
                Drug medicine=new Drug();
                medicine.GenericName = tbGenericName.Text;
                medicine.BrandName = tbBrandName.Text;
                medicine.Box = Convert.ToInt32(tbNumberInPack.Text);
                medicine.NumberPackInBox = Convert.ToInt32(tbNumberInBox.Text);
                medicine.Quantity = Convert.ToInt32(tbTotalQuantity.Text);
                medicine.ExpiryDate = tbExpiringDate.DisplayDate;
                medicine.NumberinPack = Convert.ToInt32(tbBox.Text);
                medicine.DosageFormId = (int)cbDosageForm.SelectedValue;
                medicine.DrugFormId = (int)cbDrugType.SelectedValue;
                medicine.CategoryId = (int)cbDrugCategory.SelectedValue;
                medicine.PackagingId = (int)cbPackaging.SelectedValue;
                medicine.SupplierId = (int)cbSupplier.SelectedValue;
                new DrugRepository().SaveDrug(medicine);
            cmb.Message = $"succefully added {tbBrandName.Text}";
            cmb.Show();
                Util.Clear(this);
            }
        }
        private void AddNewDosageForm_Click(object sender, RoutedEventArgs e)
        {
            new PharAddDosageForm().ShowDialog();
        }

        private void AddNewCategory_Click(object sender, RoutedEventArgs e)
        {
            new PharAddCategory().ShowDialog();
        }

        private void AddNewType_Click(object sender, RoutedEventArgs e)
        {
            new PharAddType().ShowDialog();
        }

        private void tbBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var no = 0;
            if (string.IsNullOrWhiteSpace(tbBox.Text))
            {
                no = 1;
            }
            else
            {
                no = Convert.ToInt32(tbBox.Text);
            }
            boxnumber = no <= 1 ? 1 : no;
                GetTotalNumber();
            
        }

        private void tbNumberInBox_TextChanged(object sender, TextChangedEventArgs e)
        {


            var no = 0;
            if (string.IsNullOrWhiteSpace(tbNumberInBox.Text))
            {
                no = 1;
            }
            else
            {
                no = Convert.ToInt32(tbNumberInBox.Text);
            }
            Packnumber = no <= 1 ? 1 : no;
                GetTotalNumber();
            
        }

        private void tbNumberInPack_TextChanged(object sender, TextChangedEventArgs e)
        {
            var no = 0;
            if (string.IsNullOrWhiteSpace(tbNumberInPack.Text))
            {
                no = 1;
            }
            else
            {
                no = Convert.ToInt32(tbNumberInPack.Text);
            }
            NumberInPack = no <= 1 ? 1 : no;
            GetTotalNumber();
            
        }
    }
}
