using AutoMapper;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.DTO.StatisticDTO;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Mappers.PlayerStatistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Mappers.PlayerStatistic
{
    public class UserToTrackedPlayerListDTOMapperProfile : Profile
    {
        public UserToTrackedPlayerListDTOMapperProfile()
        {
            CreateMap<User, TrackedPlayerListDTO>()
                .ForMember(dest => dest.TrackedPlayerList, opt => opt.MapFrom(src => src.TrackedPlayers.Select(u => new TrackedPlayerShortInfoDTO()
                {
                    TrackedPlayerId = u.TrackedPlayerId,
                    Nickname = u.Player.CurrentNickname
                })));
        }
    }
}