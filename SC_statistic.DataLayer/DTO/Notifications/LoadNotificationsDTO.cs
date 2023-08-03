using SC_statistic.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Notifications
{
    public class LoadNotificationsDTO
    {
        public long? NotificationId { get; set; }
        public NotificationType? Type { get; set; }
    }
}
