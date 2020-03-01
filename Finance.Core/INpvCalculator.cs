using System;
using System.Collections.Generic;
using System.Text;
using Finance.Entity.Models;

namespace Finance.Core
{
    public interface INpvCalculator
    {
        NpvDTO Compute(NpvDTO npvDTO);
    }
}
