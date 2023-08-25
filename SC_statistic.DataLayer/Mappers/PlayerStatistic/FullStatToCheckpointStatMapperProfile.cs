using AutoMapper;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.DTO.StatisticDTO;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Mappers.PlayerStatistic
{
    public class FullStatToCheckpointStatMapperProfile : Profile
    {
        public FullStatToCheckpointStatMapperProfile()
        {
            CreateMap<FullStat, CheckpointStat>()
                .ForMember(dest => dest.GamePlayed, opt => opt.MapFrom(src => src.Pvp.GamePlayed))
                .ForMember(dest => dest.GameWin, opt => opt.MapFrom(src => src.Pvp.GameWin))
                .ForMember(dest => dest.TotalKill, opt => opt.MapFrom(src => src.Pvp.TotalKill))
                .ForMember(dest => dest.TotalAssists, opt => opt.MapFrom(src => src.Pvp.TotalAssists))
                .ForMember(dest => dest.TotalDeath, opt => opt.MapFrom(src => src.Pvp.TotalDeath));
        }
    }
}