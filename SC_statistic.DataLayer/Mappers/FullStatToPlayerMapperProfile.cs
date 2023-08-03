using AutoMapper;
using SC_statistic.DataLayer.DTO.Notifications;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Mappers
{
    public class FullStatToPlayerMapperProfile : Profile
    {
        public FullStatToPlayerMapperProfile()
        {
            CreateMap<FullStat, Player>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.CurrentNickname, opt => opt.MapFrom(src => src.NickName))
                .ForMember(dest => dest.IsInformationCorrect, opt => opt.MapFrom(src => true));
        }
    }
}
