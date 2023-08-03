using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Entities
{
    public class PlayerNicknameHistory
    {
        public long PlayerNicknameHistoryId { get; set; }
        public long PlayerId { get; set; }
        public string Nickname { get; set; } = null!;
        public DateTime Date { get; set; }

        public Player Player { get; set; } = null!;

        public PlayerNicknameHistory() { }
        public PlayerNicknameHistory(long playerId, string nickname, DateTime date)
        {
            PlayerId = playerId;
            Nickname = nickname;
            Date = date;
        }
    }
}
