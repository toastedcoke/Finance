using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Finance.Data;
using Finance.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finance
{
    public class EditModel : PageModel
    {
        private readonly INpvData npvData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Npv Npv { get; set; }

        public EditModel(INpvData npvData, IHtmlHelper htmlHelper)
        {
            this.npvData = npvData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int npvId)
        {
            Npv = npvData.GetByNpvId(npvId);

            if(Npv == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            Npv.CashFlows = new List<CashFlow>();

            for (int i = 0; i < Request.Form["y.Id"].Count; i++)
            {
                Npv.CashFlows.Add(new CashFlow
                {
                    Id = Convert.ToInt32(Request.Form["y.Id"][i]),
                    Amount = Convert.ToDouble(Request.Form["y.Amount"][i])
                });
            }

            if (ModelState.IsValid)
            {
                npvData.Update(Npv);
                npvData.Commit();
                return RedirectToPage("./Detail", new { npvId = Npv.NpvId });
            }

            
            return Page();
        }
    }
}