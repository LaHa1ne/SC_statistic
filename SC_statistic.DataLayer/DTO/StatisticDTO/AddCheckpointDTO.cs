using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.StatisticDTO
{
    public class AddCheckpointDTO
    {
        public Guid SessionId { get; set; }
        public CheckpointDTO NewCheckpoint { get; set; } = null!;
        public CheckpointDTO AverageValues { get; set; } = null!;
    }
}
