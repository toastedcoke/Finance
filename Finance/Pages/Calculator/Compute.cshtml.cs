using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Finance.Data;
using Finance.Entity.Models;
using Finance.Entity.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Finance.Core;
using AutoMapper;

namespace Finance
{
    public class ComputeModel : PageModel
    {
        private readonly INpvData npvData;
        private readonly IMapper mapper;

        [BindProperty]
        public NpvDTO dto { get; set; }

        public ComputeModel(INpvData npvData, IMapper mapper)
        {
            this.npvData = npvData;
            this.mapper = mapper;
        }

        public IActionResult OnGet()
        {
            try
            {
                dto = TempData.Get<NpvDTO>("npvDTO");

                if (dto == null)
                {
                    return RedirectToPage("./NotFound");
                }

                NpvValidate validate = new NpvValidate();
                NpvCalculator calc = new NpvCalculator(validate);
                dto = calc.Compute(dto);

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
                dto.CashFlows.Add(new CashFlowDTO
                {
                    Amount = string.IsNullOrEmpty(Request.Form["n.Amount"][i]) ? 0 : Convert.ToDouble(Request.Form["n.Amount"][i]),
                    NpvAmount = string.IsNullOrEmpty(Request.Form["n.NpvAmount"][i]) ? 0 : Convert.ToDouble(Request.Form["n.NpvAmount"][i])
                });
            }
            if (ModelState.IsValid)
            {

                var npv = mapper.Map<Npv>(dto);

                if (dto.NpvId > 0)
                {
                    npvData.Update(npv);
                }
                else
                {
                    npvData.Add(npv);
                }

                return RedirectToPage("./Detail", new { npvId = npv.NpvId });
            }

            return RedirectToPage("./Error");
        }

    }
}