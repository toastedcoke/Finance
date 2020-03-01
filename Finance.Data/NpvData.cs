using System;
using System.Collections.Generic;
using System.Text;
using Finance.Entity.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Finance.Data
{
    public class NpvData : INpvData
    {
        private readonly FinanceDbContext db;


        public NpvData(FinanceDbContext db)
        {
            this.db = db;
        }

        public Npv Add(Npv npv)
        {
            db.Add(npv);
            AddCashflow(npv.CashFlows);
            db.SaveChanges();
            return npv;
        }

        public List<CashFlow> AddCashflow(List<CashFlow> cashFlow)
        {
            db.CashFlows.AddRange(cashFlow);
            return cashFlow;
        }


        public Npv Delete(int id)
        {
            var npv = GetByNpvId(id);

            if (npv != null)
            {
                db.Npvs.Remove(npv);
            }

            if (npv.CashFlows != null)
            {
                db.CashFlows.RemoveRange(npv.CashFlows);
            }
            db.SaveChanges();

            return npv;
        }

        public Npv GetByNpvId(int id)
        {
            //var npv= db.Npvs.Find(id);
            var npv = new Npv();

            var entity = db.Npvs.Where(n => n.NpvId == id).Include(c => c.CashFlows).FirstOrDefault();

            npv = entity;

            return npv;
        }

        public List<Npv> GetNpvByName(string name)
        {
            var query = from n in db.Npvs
                        where string.IsNullOrEmpty(name) || n.Name.ToUpper().Contains(name.ToUpper())
                        orderby n.Name
                        select n;

            return query.ToList();
        }

        public Npv Update(Npv updatedNpv)
        {
            var entity = GetByNpvId(updatedNpv.NpvId);

            if (entity != null)
            {
                entity.Name = updatedNpv.Name;
                entity.InitialValue = updatedNpv.InitialValue;
                entity.TotalNpvAmount = updatedNpv.TotalNpvAmount;
                entity.CashFlows = updatedNpv.CashFlows;

                db.SaveChanges();
                return updatedNpv;
            }
            else
            {
                return null;
            }


            //var entity = db.Npvs.Attach(updatedNpv);
            //entity.State = EntityState.Modified;

            //foreach (var c in updatedNpv.CashFlows)
            //{
            //    db.CashFlows.Attach(c);
            //    db.Entry(c).State = EntityState.Modified;
            //}
            
        }

    }
}
