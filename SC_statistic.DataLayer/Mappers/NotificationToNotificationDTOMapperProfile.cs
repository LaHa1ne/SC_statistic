using AutoMapper;
using SC_statistic.DataLayer.DTO.Notifications;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Mappers
{
    public class NotificationToNotificationDTOMapperProfile : Profile
    {
        public NotificationToNotificationDTOMapperProfile()
        {
            CreateMap<Notification, NotificationDTO>()
                .ForMember(dest => dest.NotificationId, opt => opt.MapFrom(src => src.NotificationId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("d MMMM yyyy")))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

        }
    }
}
