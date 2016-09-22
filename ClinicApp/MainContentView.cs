using System;
using System.Windows.Controls;
using ClinicApp;

namespace Package_Manager.View
{
    public class MainContentView: ViewModelBase
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

        public void NavigateToAddNewDrugControl()
        {
            //var addDrugControl = new AddDrugControl();
            //navigateToView(addDrugControl);
        }
    }
}
