using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Finance.Data;
using AutoMapper;
using Finance.Entity.Models;

namespace Finance
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly INpvData npvData;
        private readonly IMapper mapper;

        public IEnumerable<NpvDTO> Npvs { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config, INpvData npvData, IMapper mapper)
        {
            this.config = config;
            this.npvData = npvData;
            this.mapper = mapper;
        }


        public void OnGet()
        {
            var result = npvData.GetNpvByName(SearchTerm);
            Npvs = mapper.Map<IEnumerable<NpvDTO>>(result);
        }
    }
}