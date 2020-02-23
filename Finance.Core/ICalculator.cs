using System;
using System.Collections.Generic;
using System.Text;
using Finance.Entity;

namespace Finance.Core
{
    public interface ICalculator
    {
        Npv Compute(Npv npv);
    }
}
