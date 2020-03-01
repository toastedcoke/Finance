using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Finance.Entity.Entities;

namespace Finance.Data
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions options) : base(options)
        {
               
        }

        public DbSet<Npv> Npvs { get; set; }
        public DbSet<CashFlow> CashFlows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Npv>().ToTable("Npv");
            modelBuilder.Entity<CashFlow>().ToTable("CashFlow");
        }
    }
}
