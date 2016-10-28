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
        Drug medicine = new Drug();
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
        public PharAddDrug(Drug updateDrug)
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
            var stock = DrugRepository.GetDrugStock(updateDrug);
             medicine.Id = updateDrug.Id;
             tbGenericName.Text= updateDrug.GenericName;
             tbBrandName.Text = updateDrug.BrandName;
             tbNumberInPack.Text= stock.Box.ToString();
             tbNumberInBox.Text= stock.NumberPackInBox.ToString();
             tbTotalQuantity.Text= updateDrug.Quantity.ToString();
             tbExpiringDate.DisplayDate= stock.ExpiryDate;
             tbBox.Text= stock.NumberinPack.ToString();
             cbDosageForm.SelectedValue= stock.DosageFormId;
             cbDrugType.SelectedValue= stock.DrugFormId;
             cbDrugCategory.SelectedValue= stock.CategoryId;
             cbPackaging.SelectedValue= stock.PackagingId;
             cbSupplier.SelectedValue= stock.SupplierId;
             cbPackaging.SelectedValue= stock.PackagingId;


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
                var stock=new DrugStock();
                medicine.GenericName = tbGenericName.Text;
                medicine.BrandName = tbBrandName.Text;
                medicine.Quantity = Convert.ToInt32(tbTotalQuantity.Text);
                stock.Box = Convert.ToInt32(tbNumberInPack.Text);
                stock.NumberPackInBox = Convert.ToInt32(tbNumberInBox.Text);
                stock.ExpiryDate = tbExpiringDate.DisplayDate;
                stock.NumberinPack = Convert.ToInt32(tbBox.Text);
                stock.DosageFormId = (int)cbDosageForm.SelectedValue;
                stock.DrugFormId = (int)cbDrugType.SelectedValue;
                stock.CategoryId = (int)cbDrugCategory.SelectedValue;
                stock.PackagingId = (int)cbPackaging.SelectedValue;
                stock.SupplierId = (int)cbSupplier.SelectedValue;
                stock.PackagingId = (int)cbPackaging.SelectedValue;
                stock.Quantity=Convert.ToInt32(tbTotalQuantity.Text);
                var repo = new DrugRepository();

                if (medicine.Id.HasValue)
                {
                    stock.DrugId = Convert.ToInt32(medicine.Id);
                    repo.UpdateDrug(medicine);
                    repo.AddStock(stock);

                }
                else
                {
                    stock.DrugId= repo.SaveDrug(medicine);
                    repo.AddStock(stock);
                }

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
