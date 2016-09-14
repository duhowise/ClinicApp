using System.Collections.ObjectModel;

namespace ClinicApp
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