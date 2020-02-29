using System;
using System.Collections.Generic;
using System.Text;
using Finance.Entity;

namespace Finance.Core
{
    public interface INpvCalculator
    {
        Npv Compute(Npv npv);
    }
}
