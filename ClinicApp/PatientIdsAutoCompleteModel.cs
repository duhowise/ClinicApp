using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp
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
