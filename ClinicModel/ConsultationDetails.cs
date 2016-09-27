namespace ClinicModel
{
    public class ConsultationDetails:Consultation
    {
        public virtual Patient Patient { get; set; }
    }
}