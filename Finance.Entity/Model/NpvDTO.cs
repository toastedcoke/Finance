using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Finance.Entity.Model
{
    public class NpvDTO
    {
        public int NpvId { get; set; }
        public string Name { get; set; }
        public double InitialValue { get; set; }
        public double IncrementRate { get; set; }

        public double LowerRate { get; set; }

        public double UpperRate { get; set; }

        public List<CashFlow> CashFlows { get; set; }
        public double TotalNpvAmount { get; set; }
        public NpvDTO()
        {
            CashFlows = new List<CashFlow>();
        }
    }
}
