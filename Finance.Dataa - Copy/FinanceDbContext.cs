using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Finance.Core;

namespace Finance.Data
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<Npv> Npvs { get; set; }
        public DbSet<CashFlow> CashFlows { get; set; }
    }
}
