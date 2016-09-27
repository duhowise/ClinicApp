using System.Collections.ObjectModel;

namespace ClinicApp.Logic
{
    class PatientIdsAutoCompleteModel
    {
        PatientIdsAutoCompleteData source = new PatientIdsAutoCompleteData();
        private ObservableCollection<SinglePatientId> patientIdAutoCompleteList = new ObservableCollection<SinglePatientId>();

        public ObservableCollection<SinglePatientId> PatientIdsAutoCompleteList
        {
            get
            {
                return patientIdAutoCompleteList;
            }
        }

        public PatientIdsAutoCompleteModel()
        {
            foreach (var singleDrugData in source.GetPatientIdAutoCompleteData())
            {
                patientIdAutoCompleteList.Add(singleDrugData);
            }
        }
    }
}
