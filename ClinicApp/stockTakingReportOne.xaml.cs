using System;
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
using Telerik.ReportViewer.Wpf;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for stockTakingReportOne.xaml
    /// </summary>
    public partial class stockTakingReportOne : Window
    {
        public stockTakingReportOne()
        {
            InitializeComponent();
            
            this.Loaded += StockTakingReportOne_Loaded;
              }

        private void StockTakingReportOne_Loaded(object sender, RoutedEventArgs e)
        {
            StockTakingReport.ReportSource = new ClinicReport.ClinicReport();
            StockTakingReport.RefreshReport();
            StockTakingReport.ViewMode=ViewMode.PrintPreview;
            StockTakingReport.ZoomMode=ZoomMode.PageWidth;

        }
    }
}
