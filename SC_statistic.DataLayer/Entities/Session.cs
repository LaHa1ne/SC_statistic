using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Statistic
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public Guid TrackedPlayerId { get; set; }
        public TrackedPlayer TrackedPlayer { get; set; } = null!;
        public List<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();
    }
}
