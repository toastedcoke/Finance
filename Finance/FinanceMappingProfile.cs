using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Finance.Entity.Models;
using Finance.Entity.Entities;
using System.Linq;

namespace Finance
{
    public class FinanceMappingProfile : Profile
    {
        public FinanceMappingProfile()
        {
            //CreateMap<Npv, NpvDTO>()
            //    .ForMember(c => c.NpvId, opt => opt.MapFrom(m => m.NpvId))
            //    .ReverseMap();
            CreateMap<CashFlow, CashFlowDTO>()
                .ReverseMap();

            CreateMap<Npv, NpvDTO>().ForMember(x => x.CashFlows, o => o.MapFrom(y => y.CashFlows))
                .ReverseMap();

  
        }
    }
}
