using System;
using System.Collections.Generic;
using System.Text;
using Finance.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Finance.Data
{
    public interface INpvData
    {
        IEnumerable<Npv> GetNpvByName(string name);
        Npv GetByNpvId(int id);
        Npv Update(Npv updatedNpv);
        Npv Add(Npv newNpv);
        //List<CashFlow> AddCashflow(List<CashFlow> cashFlow);
        Npv Delete(int id);
    }

  



}
