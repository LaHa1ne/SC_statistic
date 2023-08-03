using SC_statistic.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC_statistic.DataLayer.Responses;
using SC_statistic.DataLayer.DTO.Notifications;

namespace SC_statistic.Services.Interfaces
{
    public interface INotificationService
    {
        Task<BaseResponse<NotificationListDTO>> GetNotifications();
        Task<BaseResponse<NotificationListDTO>> GetNotificationsWithSelectedType(LoadNotificationsDTO loadNotificationsDTO); 
    }
}
