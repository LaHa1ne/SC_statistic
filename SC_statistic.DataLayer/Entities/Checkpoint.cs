using SC_statistic.DataLayer.DTO.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Collections.Specialized.BitVector32;

namespace SC_statistic.DataLayer.Entities
{
    public class Checkpoint
    {
        public Guid CheckpointId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool IsStarted { get; set; } = false;
        public CheckpointStat CheckpointStat { get; set; } = null!;

        public Guid SessionId { get; set; }
        public Session Session { get; set; } = null!;

    }
}
