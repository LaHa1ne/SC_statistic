using AutoMapper;
using SC_statistic.DataLayer.DTO.Corporation;
using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Mappers.Corporations
{
    public class CorporationToCorporationDTOMapperProfile : Profile
    {
        public CorporationToCorporationDTOMapperProfile()
        {
            CreateMap<Corporation, CorporationDTO>()
                .ForMember(dest => dest.CurrentName, opt => opt.MapFrom(src => src.CurrentName))
                .ForMember(dest => dest.CurrentTag, opt => opt.MapFrom(src => src.CurrentTag))
                .ForMember(dest => dest.PvpRating, opt => opt.MapFrom(src => src.PvpRating))
                .ForMember(dest => dest.PveRating, opt => opt.MapFrom(src => src.PveRating))
                .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players.Select(p => new PlayerInCorporationDTO()
                {
                    CurrentNickname = p.CurrentNickname,
                    IsInformationCorrect = p.IsInformationCorrect
                })));
        }
    }
}