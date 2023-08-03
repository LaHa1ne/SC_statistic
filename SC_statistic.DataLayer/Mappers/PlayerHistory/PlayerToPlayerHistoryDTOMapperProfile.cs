using AutoMapper;
using SC_statistic.DataLayer.DTO.Notifications;
using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Mappers.PlayerHistory
{
    public class PlayerToPlayerHistoryDTOMapperProfile : Profile
    {
        public PlayerToPlayerHistoryDTOMapperProfile()
        {
            CreateMap<Player, PlayerHistoryDTO>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId))
                .ForMember(dest => dest.IsInformationCorrect, opt => opt.MapFrom(src => src.IsInformationCorrect))
                .ForMember(dest => dest.CurrentNickname, opt => opt.MapFrom(src => src.CurrentNickname))
                .ForMember(dest => dest.CurrentCorpName, opt => opt.MapFrom(src => src.CurrentCorporation == null ? "" : $"{src.CurrentCorporation.CurrentName}[{src.CurrentCorporation.CurrentTag}]"))
                .ForMember(dest => dest.NicknameHistory, opt => opt.MapFrom(src => src.NicknameHistory.Select(nh => new PlayerNicknameHistoryDTO()
                {
                    Nickname = nh.Nickname,
                    Date = nh.Date.ToString("d MMMM yyyy")
                })))
                .ForMember(dest => dest.CorporationHistory, opt => opt.MapFrom(src => src.CorporationHistory.Select(ch => new PlayerCorporationHistoryDTO()
                {
                    NameAndTag = ch.Corporation == null ? "Без корпорации" : $"{ch.Corporation.CurrentName}[{ch.Corporation.CurrentTag}]",
                    Date = ch.Date.ToString("d MMMM yyyy")
                })));
        }
    }
}