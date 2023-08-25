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
    public class SessionToSessionDTOMapperProfile : Profile
    {
        public SessionToSessionDTOMapperProfile() 
        {
            CreateMap<Session, SessionDTO>()
                .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.SessionId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(dest => dest.StartCheckpoint, opt => opt.MapFrom(src => new CheckpointDTO(src.Checkpoints.First())))
                .ForMember(dest => dest.Checkpoints, opt => opt.MapFrom(src => CheckpointListConvertToCheckpointDTOList(src.Checkpoints)))
                .ForMember(dest => dest.AverageValues, opt => opt.MapFrom(src => new CheckpointDTO(src.Checkpoints.First(), src.Checkpoints.Last())));
        }

        List<CheckpointDTO> CheckpointListConvertToCheckpointDTOList(List<Checkpoint> checkpointList)
        {
            var checkpointDTOList = new List<CheckpointDTO>();

            for (int i = 1; i < checkpointList.Count; i++)
            {
                checkpointDTOList.Add(new CheckpointDTO(checkpointList[i - 1], checkpointList[i]));
            }

            return checkpointDTOList;
        }

    }
}