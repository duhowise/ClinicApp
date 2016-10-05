using System;
using System.Windows.Controls;
using ClinicApp.Pharmacist;
using ViewModelBase = Package_Manager.View.ViewModelBase;

namespace ClinicApp.Logic
{
    public class MainContentView : ViewModelBase
    {
        private readonly Action<UserControl> navigateToView;

        public MainContentView(Action<UserControl> navigateToView)
        {
            this.navigateToView = navigateToView;
            Initializate();
        }



        public void Initializate()
        {

        }

        public void NavigateTopharAdminDashboardControl()
        {
            var pharAdminDashboard = new PharAdminDashboard();
            navigateToView(pharAdminDashboard);
        }

        public void NavigateToPharPatientDetailDispensaryControl()
        {
            var pharPatientDetailDispensary = new PharPatientDetailsDispensary();
            navigateToView(pharPatientDetailDispensary);
        }
    }
}
