using SC_statistic.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Notifications
{
    public class NotificationDTO
    {
        public long NotificationId { get; set; }
        public string Text { get; set; } = null!;
        public string Date { get; set; } = null!;
        public NotificationType Type { get; set; }
    }
}
