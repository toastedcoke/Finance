using Finance.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Core
{
    public interface INpvValidate
    {
        bool Validate(NpvDTO npv);
    }
}
