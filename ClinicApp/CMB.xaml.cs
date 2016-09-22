using System.Windows;

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
