using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Notifications
{
    public class NotificationListDTO
    {
        public List<NotificationDTO> Notifications = null!;
        public bool HasMoreNotifications { get; set; }

    }
}
