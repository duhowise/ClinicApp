﻿using System;

namespace ClinicModel
{
    public class DrugStock
    {
            public int? Id { get; set; }
            public int DrugId { get; set; }
            public int Box { get; set; }
            public int NumberPackInBox { get; set; }
            public int Quantity { get; set; }
            public DateTime ExpiryDate { get; set; }
            public int NumberinPack { get; set; }
            public int DosageFormId { get; set; }
            public int DrugFormId { get; set; }
            public int CategoryId { get; set; }
            public DateTime AddedDate { get; set; }
            public int PackagingId { get; set; }
            public int SupplierId { get; set; }
        
    }
}