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
    public class SessionToAddCheckpointDTOMapperProfile : Profile
    {
        public SessionToAddCheckpointDTOMapperProfile() 
        {
            CreateMap<Session, AddCheckpointDTO>()
                .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.SessionId))
                .ForMember(dest => dest.NewCheckpoint, opt => opt.MapFrom(src => new CheckpointDTO(src.Checkpoints[src.Checkpoints.Count - 2], src.Checkpoints.Last())))
                .ForMember(dest => dest.AverageValues, opt => opt.MapFrom(src => new CheckpointDTO(src.Checkpoints.First(), src.Checkpoints.Last())));
        }
    }
}