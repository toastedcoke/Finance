using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Finance.Entity.Models
{
    public class NpvDTO
    {
        public int NpvId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Initial Value")]
        public double InitialValue { get; set; }
        [Required]
        [Display(Name = "Incremental Rate")]
        public decimal IncrementRate { get; set; }
        [Required]
        [Display(Name = "Lower Rate")]
        public decimal LowerRate { get; set; }
        [Required]
        [Display(Name = "Upper Rate")]
        public decimal UpperRate { get; set; }
    
        public List<CashFlowDTO> CashFlows { get; set; }
        [Display(Name = "Current Npv Amount")]
        public double? TotalNpvAmount { get; set; }
        public NpvDTO()
        {
            CashFlows = new List<CashFlowDTO>();
        }
    }
}
