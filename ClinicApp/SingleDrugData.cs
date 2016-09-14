namespace ClinicApp
{
    public class SingleDrugData
    {
        private string Name;

        public string DrugName
        {
            get
            {
                return Name;
            }

            set
            {
                if (Name != value)
                {
                    Name = value;
                }
            }
        }
    }
}