using System;
using System.Collections.Generic;
using System.Text;
using Finance.Entity;
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
            return npv;
        }

        public List<CashFlow> AddCashflow(List<CashFlow> cashFlow)
        {
            db.CashFlows.AddRange(cashFlow);
            return cashFlow;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Npv Delete(int id)
        {
            var npv = GetByNpvId(id);

            if (npv != null)
            {
                db.Npvs.Remove(npv);
            }

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

        public IEnumerable<Npv> GetNpvByName(string name)
        {
            var query = from n in db.Npvs
                        where string.IsNullOrEmpty(name) || n.Name.ToUpper().Contains(name.ToUpper())
                        orderby n.Name
                        select n;

            return query;
        }

        public Npv Update(Npv updatedNpv)
        {
            var entity = db.Npvs.Attach(updatedNpv);
            entity.State = EntityState.Modified;

            foreach(var c in updatedNpv.CashFlows)
            {
                db.CashFlows.Attach(c);
                db.Entry(c).State = EntityState.Modified;
            }
            return updatedNpv;
        }

    }
}
