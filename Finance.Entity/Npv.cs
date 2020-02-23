using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finance.Entity
{
    public class Npv
    {
        public int NpvId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double InitialValue { get; set; }
        [Required]
        public double IncrementRate { get; set; }
        
        [Required]
        public double LowerRate { get; set; }

        [Required]
        public double UpperRate { get; set; }

        public List<CashFlow> CashFlows { get; set; }
        public double TotalNpvAmount { get; set; }
        public Npv()
        {
            CashFlows = new List<CashFlow>();
        }
    }
}
