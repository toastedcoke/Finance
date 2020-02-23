using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Finance.Entity;
using Finance.Data;

namespace Finance
{
    public class DetailModel : PageModel
    {
        public Npv Npv { get; set; }
        public INpvData NpvData { get; set; }

        public DetailModel(INpvData npvData)
        {
            this.NpvData = npvData;
        }
        public IActionResult OnGet(int npvId)
        {
            Npv = NpvData.GetByNpvId(npvId);

            if (Npv == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }
    }
}