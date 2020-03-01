using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finance.Entity.Entities
{
    public class Npv
    {
        public int NpvId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double InitialValue { get; set; }
        [Required]
        public decimal IncrementRate { get; set; }
        
        [Required]
        public decimal LowerRate { get; set; }

        [Required]
        public decimal UpperRate { get; set; }

        public List<CashFlow> CashFlows { get; set; }
        public double? TotalNpvAmount { get; set; }
        public Npv()
        {
            CashFlows = new List<CashFlow>();
        }
    }
}
