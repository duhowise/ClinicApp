namespace ClinicApp.Logic
{
    class SinglePatientId
    {
        private string providedId;

        public string ProvidedId
        {
            get
            {
                return providedId;
            }

            set
            {
                if (providedId != value)
                {
                    providedId = value;
                }
            }
        }
    }
}
