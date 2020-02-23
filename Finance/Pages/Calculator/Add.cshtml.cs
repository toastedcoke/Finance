using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Finance.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Finance.Data;

namespace Finance
{
    public class AddModel : PageModel
    {
        private readonly INpvData npvData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Npv Npv { get; set; }

        public AddModel(INpvData npvData, IHtmlHelper htmlHelper)
        {
            this.npvData = npvData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int npvId)
        {
            Npv = new Npv();
            Npv.CashFlows = new List<CashFlow>();

            return Page();
        }

        public IActionResult OnPost()
        {
            Npv.CashFlows = new List<CashFlow>();

            for (int i = 0; i < Request.Form["DynamicTextBox"].Count; i++)
            {
                Npv.CashFlows.Add(new CashFlow
                {
                    Amount = Convert.ToDouble(Request.Form["DynamicTextBox"][i])
                });
            }

            if (ModelState.IsValid)
            {
                TempData["npv"] = JsonConvert.SerializeObject(Npv);
                return RedirectToPage("./Compute", new { npv = Npv });
            }

            return Page();
        }

       
    }
}
