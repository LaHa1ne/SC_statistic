using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.Interfaces
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<Notification> GetByNotificationId(long notificationId);
        Task<List<Notification>> GetNotificationsWithSelectedType(int num_notifications_to_load, NotificationType? notificationType);
        Task<List<Notification>> GetMoreNotificationsWithSelectedType(int num_notifications_to_load, NotificationType? notificationType, DateTime dateBefore);
    }
}
