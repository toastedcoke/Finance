using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Finance.Data;
using Finance.Entity.Models;
using Finance.Entity.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Finance.Core;
using AutoMapper;

namespace Finance
{
    public class EditModel : PageModel
    {
        private readonly INpvData npvData;
        private readonly IHtmlHelper htmlHelper;
        private readonly IMapper mapper;

        [BindProperty]
        public NpvDTO dto { get; set; }

        public EditModel(INpvData npvData, IHtmlHelper htmlHelper, IMapper mapper)
        {
            this.npvData = npvData;
            this.htmlHelper = htmlHelper;
            this.mapper = mapper;
        }
        public IActionResult OnGet(int npvId)
        {
            var result = npvData.GetByNpvId(npvId);
            dto = mapper.Map<NpvDTO>(result);

            if (dto == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            dto.CashFlows = new List<CashFlowDTO>();

            for (int i = 0; i < Request.Form["y.Id"].Count; i++)
            {
                dto.CashFlows.Add(new CashFlowDTO
                {
                    Id = Convert.ToInt32(Request.Form["y.Id"][i]),
                    Amount = Convert.ToDouble(Request.Form["y.Amount"][i])
                });
            }
            NpvValidate validate = new NpvValidate();
            NpvCalculator calc = new NpvCalculator(validate);
            dto = calc.Compute(dto);

            if (ModelState.IsValid)
            {
                var npv = mapper.Map<Npv>(dto);
                npvData.Update(npv);

                return RedirectToPage("./Detail", new { npvId = dto.NpvId });

            }

            
            return Page();
        }
    }
}