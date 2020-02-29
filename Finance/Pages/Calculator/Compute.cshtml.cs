using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Finance.Data;
using Finance.Entity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Finance.Core;

namespace Finance
{
    public class ComputeModel : PageModel
    {
        private readonly INpvData npvData;

        [BindProperty]
        public Npv Npv { get; set; }

        public ComputeModel(INpvData npvData)
        {
            this.npvData = npvData;
        }

        public IActionResult OnGet()
        {
            try
            {
                Npv = TempData.Get<Npv>("npv");

                if (Npv == null)
                {
                    return RedirectToPage("./NotFound");
                }

                NpvValidate validate = new NpvValidate();
                NpvCalculator calc = new NpvCalculator(validate);
                Npv = calc.Compute(Npv);

                return Page();
            }
            catch (Exception e)
            {
                return RedirectToPage("./Error");
            }
        }

        public IActionResult OnPost()
        {
            for (int i = 0; i < Request.Form["n.Id"].Count; i++)
            {
                Npv.CashFlows.Add(new CashFlow
                {
                    Amount = Convert.ToDouble(Request.Form["n.Amount"][i]),
                    NpvAmount = Convert.ToDouble(Request.Form["n.NpvAmount"][i])
                });
            }
            if (ModelState.IsValid)
            {
                if (Npv.NpvId > 0)
                {
                    npvData.Update(Npv);
                }
                else
                {
                    npvData.Add(Npv);
                }

                return RedirectToPage("./Detail", new { npvId = Npv.NpvId });
            }

            return RedirectToPage("./Error");
        }

    }
}