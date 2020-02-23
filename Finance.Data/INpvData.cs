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
        List<CashFlow> AddCashflow(List<CashFlow> cashFlow);
        Npv Delete(int id);
        int Commit();
    }

  


    public class NpvData1 : INpvData
    {
        List<Npv> Npvs;
        public NpvData1()
        {
            Npvs = new List<Npv>()
            {
                new Npv{ NpvId=1, Name="Cashflow 1", IncrementRate = 25, UpperRate=1, LowerRate =2, InitialValue =10000,
                    CashFlows = new List<CashFlow>(){
                        new CashFlow { Id = 1, NpvId =1 , Amount = 1000},
                        new CashFlow { Id = 2, NpvId =1 , Amount = 2000} }},
                new Npv{ NpvId=2, Name="Cashflow 2", IncrementRate = 25, UpperRate=1, LowerRate =2, InitialValue =20000,
                    CashFlows = new List<CashFlow>(){
                        new CashFlow { Id = 1, NpvId =2 , Amount = 3000},
                        new CashFlow { Id = 2, NpvId =2 , Amount = 7000} }},
                  new Npv{ NpvId=2, Name="test 2", IncrementRate = 25, UpperRate=1, LowerRate =2, InitialValue =20000,
                    CashFlows = new List<CashFlow>(){
                        new CashFlow { Id = 1, NpvId =2 , Amount = 3000},
                        new CashFlow { Id = 2, NpvId =2 , Amount = 7000} }}
            };
        }


        public IEnumerable<Npv> GetNpvByName(string name)
        {
            return from r in Npvs
                   where string.IsNullOrEmpty(name) || r.Name.ToUpper().Contains(name.ToUpper())
                   orderby r.Name
                   select r;
        }

        public Npv GetByNpvId(int id)
        {
            return Npvs.FirstOrDefault(x => x.NpvId == id);
        }

        public Npv Add(Npv newNpv)
        {
            newNpv.NpvId = Npvs.Max(r => r.NpvId) + 1;
            Npvs.Add(newNpv);
            return newNpv;
        }

        public Npv Update(Npv updatedNpv)
        {
            var npv = Npvs.FirstOrDefault(r => r.NpvId == updatedNpv.NpvId);

            if (npv == null)
            {
                npv.Name = updatedNpv.Name;
            }

            return npv;
        }

        public int Commit()
        {
            return 0;
        }

        public Npv Delete(int id)
        {
            throw new NotImplementedException();
        }


        public List<CashFlow> AddCashflow(List<CashFlow> cashFlow)
        {
            throw new NotImplementedException();
        }
    }

}
