using System.Linq;
using System.Threading;
using System.Windows;
using MahApps.Metro;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
          
            this.InitializeComponent();

            Thread.Sleep(5000);
        }
    }

    internal class LineAdorner
    {
        public LineAdorner()
        {
        }
    }
}
