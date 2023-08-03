using Microsoft.EntityFrameworkCore;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Notification> GetByNotificationId(long notificationId)
        {
            return await _db.Notifications.FirstOrDefaultAsync(x => x.NotificationId == notificationId);
        }
        public async Task<List<Notification>> GetNotificationsWithSelectedType(int num_notifications_to_load, NotificationType? notificationType)
        {
            if (notificationType != null)
                return await _db.Notifications.Where(n => n.Type == notificationType.Value).OrderByDescending(n => n.Date).ThenByDescending(n => n.NotificationId).Take(num_notifications_to_load).ToListAsync();
            return await _db.Notifications.OrderByDescending(n => n.Date).ThenByDescending(n => n.NotificationId).Take(num_notifications_to_load).ToListAsync();
        }
        public async Task<List<Notification>> GetMoreNotificationsWithSelectedType(int num_notifications_to_load, NotificationType? notificationType, DateTime dateBefore)
        {
            if (notificationType != null)
                return await _db.Notifications.Where(n => n.Type == notificationType.Value && n.Date < dateBefore).OrderByDescending(n => n.Date).ThenByDescending(n=>n.NotificationId).Take(num_notifications_to_load).ToListAsync();
            return await _db.Notifications.Where(n=>n.Date < dateBefore).OrderByDescending(n => n.Date).ThenByDescending(n => n.NotificationId).Take(num_notifications_to_load).ToListAsync();
        }
    }
}
