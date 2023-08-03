using SC_statistic.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Entities
{
    public class Notification
    {
        public long NotificationId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
        public NotificationType Type { get; set; }

        public Notification() { }
        public Notification(NotificationType type, string str1, string str2 = "", string str3 = "")
        {
            switch (type)
            {
                case NotificationType.PlayerChangedNickname:
                    Text = string.IsNullOrEmpty(str2) ? $"Игрок {str1} изменил игровое имя" : $"Игрок {str1} изменил игровое имя на {str2}";
                    break;

                case NotificationType.CorporationChangedName:
                    Text = $"Корпорация {str1} изменила имя на {str2}";
                    break;

                case NotificationType.PlayerChangedCorporation:
                    Text = $"Игрок {str1} ранее " + (string.IsNullOrEmpty(str2) ? "не состоял в корпорации. " : $"состоял в корпорации {str2}. ") + (string.IsNullOrEmpty(str3) ? "В данный момент игрок без корпорации" : $"Текущая корпорация {str3}");
                    break;
                default:
                    Text = str1;
                    break;
            }
            Date = DateTime.Now;
            Type = type;
        }
    }
}
