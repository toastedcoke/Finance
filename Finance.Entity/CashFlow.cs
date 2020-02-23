using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Entity
{
    public class CashFlow
    {
        public int Id { get; set; }
        public int NpvId { get; set; }
        public double Amount { get; set; }
        public double NpvAmount { get; set; }
    }
}
