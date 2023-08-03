using AutoMapper;
using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Mappers.PlayerHistory
{
    public class PlayerCorporationHistoryToPlayerCorporationHistoryDTOMapperProfile : Profile
    {
        public PlayerCorporationHistoryToPlayerCorporationHistoryDTOMapperProfile()
        {
            CreateMap<PlayerCorporationHistory, PlayerCorporationHistoryDTO>()
                .ForMember(dest => dest.NameAndTag, opt => opt.MapFrom(src => src.Corporation == null ? "Без корпорации" : $"{src.Corporation.CurrentName}[{src.Corporation.CurrentTag}]"))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("d MMMM yyyy")));
        }
    }
}
