using System.Windows;
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
            //StockTakingReport.ReportSource = new ClinicReport.ClinicReport();
            //StockTakingReport.RefreshReport();
            //StockTakingReport.ViewMode=ViewMode.PrintPreview;
            //StockTakingReport.ZoomMode=ZoomMode.PageWidth;

        }
    }
}
