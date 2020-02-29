using Finance.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Core
{
    public interface INpvValidate
    {
        bool Validate(Npv npv);
    }
}
