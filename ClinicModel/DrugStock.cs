using System;

namespace ClinicModel
{
    public class DrugStock
    {
        public int Id { get; set; }
        public int DrugId { get; set; }
        public int StockQuantity { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        
    }
}