using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Finance.Entity.Models;
using Finance.Data;
using AutoMapper;

namespace Finance
{
    public class DetailModel : PageModel
    {
        private readonly IMapper mapper;

        public NpvDTO Npv { get; set; }
        public INpvData NpvData { get; set; }

        public DetailModel(INpvData npvData, IMapper mapper)
        {
            this.NpvData = npvData;
            this.mapper = mapper;
        }
        public IActionResult OnGet(int npvId)
        {
            var result = NpvData.GetByNpvId(npvId);
            Npv = mapper.Map<NpvDTO>(result);

            if (Npv == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }
    }
}