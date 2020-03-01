using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Finance.Entity.Models
{
    public class CashFlowDTO
    {
        public int Id { get; set; }
        public int NpvId { get; set; }
        [Required]
        public double Amount { get; set; }
        public double NpvAmount { get; set; }
    }
}
