using AutoMapper;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataAccessLayer.Repositories;
using SC_statistic.DataLayer.DTO.Notifications;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Enums;
using SC_statistic.DataLayer.Helpers;
using SC_statistic.DataLayer.Responses;
using SC_statistic.DataLayer.ViewModels.Account;
using SC_statistic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Services
{
    public class NotificationService : INotificationService
    {
        protected const int num_of_loaded_notifications = 5;
        protected readonly IMapper _mapper;
        protected readonly INotificationRepository _notificationRepository;

        public NotificationService(IMapper mapper, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public async Task<BaseResponse<NotificationListDTO>> GetNotifications()
        {
            try
            {
                var notifications = await _notificationRepository.GetNotificationsWithSelectedType(num_of_loaded_notifications, null);
                notifications.Reverse();
                return new BaseResponse<NotificationListDTO>()
                {
                    Data = new NotificationListDTO()
                    {
                        Notifications = _mapper.Map<List<NotificationDTO>>(notifications),
                        HasMoreNotifications = notifications.Count == num_of_loaded_notifications
                    },
                    Description = "Уведомления получены",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NotificationListDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<NotificationListDTO>> GetNotificationsWithSelectedType(LoadNotificationsDTO loadNotificationsDTO)
        {
            try
            {
                if (loadNotificationsDTO.NotificationId == null)
                {
                    var notifications = await _notificationRepository.GetNotificationsWithSelectedType(num_of_loaded_notifications, loadNotificationsDTO.Type);
                    notifications.Reverse();
                    return new BaseResponse<NotificationListDTO>()
                    {
                        Data = new NotificationListDTO()
                        {
                            Notifications = _mapper.Map<List<NotificationDTO>>(notifications),
                            HasMoreNotifications = notifications.Count == num_of_loaded_notifications
                        },
                        Description = "Уведомления получены",
                        StatusCode = HttpStatusCode.OK
                    };
                }
                else
                {
                    var notification = await _notificationRepository.GetByNotificationId(loadNotificationsDTO.NotificationId.Value);
                    var notifications = await _notificationRepository.GetMoreNotificationsWithSelectedType(num_of_loaded_notifications, loadNotificationsDTO.Type, notification.Date);
                    return new BaseResponse<NotificationListDTO>()
                    {
                        Data = new NotificationListDTO()
                        {
                            Notifications = _mapper.Map<List<NotificationDTO>>(notifications),
                            HasMoreNotifications = notifications.Count == num_of_loaded_notifications
                        },
                        Description = "Уведомления получены",
                        StatusCode = HttpStatusCode.OK
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<NotificationListDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
