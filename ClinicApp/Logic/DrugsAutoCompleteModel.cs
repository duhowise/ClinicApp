using System.Collections.ObjectModel;
using ClinicApp.Pharmacist;

namespace ClinicApp.Logic
{
    public class DrugsAutoCompleteModel
    {
         DrugAutoCompleteData source = new DrugAutoCompleteData();
        private  ObservableCollection<SingleDrugData> drugAutoCompleteList = new ObservableCollection<SingleDrugData>();

        public ObservableCollection<SingleDrugData> DrugAutoCompleteList
        {
            get
            {
                return drugAutoCompleteList;
            }
        }

        public DrugsAutoCompleteModel()
        {
            foreach (var singleDrugData in source.GetDrugAutoCompleteData())
            {
                drugAutoCompleteList.Add(singleDrugData);
            }
        }
    }
}