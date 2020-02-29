using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Finance.Data;
using Finance.Entity;

namespace Finance
{
    public class DeleteModel : PageModel
    {
        private readonly INpvData npvData;
        public Npv npv { get; set; }

        public DeleteModel(INpvData npvData)
        {
            this.npvData = npvData;
        }
        public IActionResult OnGet(int npvId)
        {
            npv = npvData.GetByNpvId(npvId);

            if (npv == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost(int npvId)
        {
            var entity = npvData.Delete(npvId);

            if (entity == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{entity.Name} deleted";
            return RedirectToPage("./List");
        }
    }
}