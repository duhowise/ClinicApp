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

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for ConfirmBox.xaml
    /// </summary>
    /// 

        public interface IConfirmable
    {
        void Confirm();
        void Cancel();
    }
    public partial class ConfirmBox : Window
    {
        private string _message;
        public ConfirmBox()
        {
            InitializeComponent();
        }

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
           
        }

       
    }
}
