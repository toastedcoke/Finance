using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Finance.Entity.Models;
using Finance.Entity.Entities;
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
        public NpvDTO dto { get; set; }


        public AddModel(INpvData npvData, IHtmlHelper htmlHelper)
        {
            this.npvData = npvData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int npvId)
        {
            dto = new NpvDTO();
            dto.CashFlows = new List<CashFlowDTO>();

            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                dto.CashFlows = new List<CashFlowDTO>();

                for (int i = 0; i < Request.Form["DynamicTextBox"].Count; i++)
                {
                    dto.CashFlows.Add(new CashFlowDTO
                    {
                        Amount = string.IsNullOrEmpty(Request.Form["DynamicTextBox"][i]) ? 0 : Convert.ToDouble(Request.Form["DynamicTextBox"][i])
                    });
                }

                if (ModelState.IsValid)
                {
                    TempData["npvDTO"] = JsonConvert.SerializeObject(dto);
                    //return RedirectToPage("./Compute", new { npv = dto });
                    return RedirectToPage("./Compute");
                }

                return Page();
            }
            catch(Exception e)
            {
                return RedirectToPage("./Error");
            }
        }

       
    }
}
