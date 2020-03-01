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
        public string Name { get; set; }
        
        public double InitialValue { get; set; }
        public double IncrementRate { get; set; }

        public double LowerRate { get; set; }

        public double UpperRate { get; set; }

        public List<CashFlowDTO> CashFlows { get; set; }
        public double? TotalNpvAmount { get; set; }
        public NpvDTO()
        {
            CashFlows = new List<CashFlowDTO>();
        }
    }
}
