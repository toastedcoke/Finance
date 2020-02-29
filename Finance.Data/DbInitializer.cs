using Finance.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FinanceDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Npvs.Any())
            {
                return;   // DB has been seeded
            }


            var npvs = new List<Npv>(){
                new Npv{  Name="Cashflow 1", IncrementRate = .25, LowerRate=1, UpperRate =2, InitialValue =10000, TotalNpvAmount = -6145.16},
                new Npv{ Name="Cashflow 2", IncrementRate = 50, LowerRate=1, UpperRate =2, InitialValue =20000, TotalNpvAmount = -1000},
                new Npv{ Name="test 3", IncrementRate = 20, LowerRate=1, UpperRate =2, InitialValue =20000, TotalNpvAmount = -1000}
            };

            foreach (Npv s in npvs)
            {
                context.Npvs.Add(s);
            }
            context.SaveChanges();

            var cashFlows= new List<CashFlow>()
            {
                new CashFlow { NpvId =1 , Amount = 1000, NpvAmount= -9009.90},
                new CashFlow { NpvId =1 , Amount = 2000, NpvAmount= -8034.43},
                new CashFlow { NpvId =1 , Amount = 3000, NpvAmount= -7078.1},
                new CashFlow { NpvId =1 , Amount = 7000, NpvAmount= -6145.16},
                new CashFlow { NpvId =2 , Amount = 3000, NpvAmount= 2700},
                new CashFlow { NpvId =2 , Amount = 7000, NpvAmount= 2700},
                new CashFlow { NpvId =3 , Amount = 3000, NpvAmount= 2700},
                new CashFlow { NpvId =3 , Amount = 7000, NpvAmount= 2700},
                new CashFlow { NpvId =3 , Amount = 3000, NpvAmount= 2700},
                new CashFlow { NpvId =3 , Amount = 3000, NpvAmount= 2700},
                new CashFlow { NpvId =3 , Amount = 7000, NpvAmount= 2700}
            };
            foreach (CashFlow c in cashFlows)
            {
                context.CashFlows.Add(c);
            }
            context.SaveChanges();

        }
    }
}
