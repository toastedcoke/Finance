using Finance.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Core
{
    public class NpvCalculator : INpvCalculator
    {
        private readonly INpvValidate validator;

        public NpvCalculator(INpvValidate validator)
        {
            this.validator = validator ?? throw new System.ArgumentNullException(nameof(validator));
        }
        public NpvDTO Compute(NpvDTO npv)
        {
            try
            {
                if (validator.Validate(npv))
                {
                    int n = npv.CashFlows.Count;

                    var rate = npv.LowerRate;
                    int i = 0;  //year

                    double total = npv.InitialValue * -1;

                    foreach (var item in npv.CashFlows)
                    {
                        i++;
                        item.NpvAmount = total + (item.Amount / Math.Pow(1 + Convert.ToDouble(rate) / 100, i));

                        rate += npv.IncrementRate;
                        total = item.NpvAmount;
                    }

                    npv.TotalNpvAmount = total;
                }else
                {
                    npv.TotalNpvAmount = null;
                    npv.CashFlows.ForEach(x => x.NpvAmount = 0);
                }

                return npv;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
