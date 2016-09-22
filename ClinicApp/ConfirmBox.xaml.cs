using System.Windows;

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
