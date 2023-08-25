using AutoMapper;
using SC_statistic.DataLayer.DTO.Notifications;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Mappers.Corporations
{
    public class CorpStatToCorporationMapperProfile : Profile
    {
        public CorpStatToCorporationMapperProfile()
        {
            CreateMap<CorpStat, Corporation>()
                .ForMember(dest => dest.CorporationId, opt => opt.MapFrom(src => src.Cid))
                .ForMember(dest => dest.CurrentName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CurrentTag, opt => opt.MapFrom(src => src.Tag))
                .ForMember(dest => dest.PvpRating, opt => opt.MapFrom(src => src.PvpRating))
                .ForMember(dest => dest.PveRating, opt => opt.MapFrom(src => src.PveRating));
        }
    }
}
