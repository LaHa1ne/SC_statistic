using SC_statistic.DataLayer.DTO.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Entities
{
    public class TrackedPlayer
    {
        public Guid TrackedPlayerId {  get; set; }
        public long PlayerId { get; set; }
        public Guid UserId { get; set; }
        public Player Player { get; set; } = null!;
        public User User { get; set; } = null!;

        public List<Session> Sessions { get; set; } = new List<Session>();

    }
}
