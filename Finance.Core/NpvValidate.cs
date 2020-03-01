using Finance.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Core
{
    public class NpvValidate : INpvValidate
    {
        public bool Validate(NpvDTO npv)
        {
            if (npv.LowerRate > npv.UpperRate)
            {
                throw new Exception("Lower rate should be lower than upper rate");
            }
            if (npv.CashFlows.Count == 0)
            {
                throw new Exception("Cash flow(s) are required.");
            }

            return true;
        }
    }
}
