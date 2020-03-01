using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Finance.Data;
using Finance.Entity.Models;
using AutoMapper;

namespace Finance
{
    public class DeleteModel : PageModel
    {
        private readonly INpvData npvData;
        private readonly IMapper mapper;

        public NpvDTO npv { get; set; }

        public DeleteModel(INpvData npvData, IMapper mapper)
        {
            this.npvData = npvData;
            this.mapper = mapper;
        }
        public IActionResult OnGet(int npvId)
        {
            var result = npvData.GetByNpvId(npvId);
            npv = mapper.Map<NpvDTO>(result);

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