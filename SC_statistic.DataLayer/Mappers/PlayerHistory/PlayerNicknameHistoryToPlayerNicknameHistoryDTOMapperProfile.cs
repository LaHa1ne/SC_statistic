using AutoMapper;
using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Mappers.PlayerHistory
{
    public class PlayerNicknameHistoryToPlayerNicknameHistoryDTOMapperProfile : Profile
    {
        public PlayerNicknameHistoryToPlayerNicknameHistoryDTOMapperProfile()
        {
            CreateMap<PlayerNicknameHistory, PlayerNicknameHistoryDTO>()
                .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Nickname))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("d MMMM yyyy")));
        }
    }
}
