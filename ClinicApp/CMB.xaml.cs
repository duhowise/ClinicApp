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
    /// Interaction logic for CMB.xaml
    /// </summary>
    public partial class CMB : Window
    {
        private string _message;
        public CMB()
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

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ShowInTaskbar = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbText.Text = Message;
        }
    }
}
