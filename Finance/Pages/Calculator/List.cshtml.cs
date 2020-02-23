using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Finance.Data;
using Finance.Entity;

namespace Finance
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly INpvData npvData;

        public IEnumerable<Npv> Npvs { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config, INpvData npvData)
        {
            this.config = config;
            this.npvData = npvData;
        }


        public void OnGet()
        {
            Npvs = npvData.GetNpvByName(SearchTerm);
        }
    }
}