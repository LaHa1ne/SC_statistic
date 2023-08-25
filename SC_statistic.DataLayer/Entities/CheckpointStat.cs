using Newtonsoft.Json;
using SC_statistic.DataLayer.DTO.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Entities
{
    public class CheckpointStat
    {
        public int GamePlayed { get; set; }
        public int GameWin { get; set; }
        public int TotalKill { get; set; }
        public int TotalAssists { get; set; }
        public int TotalDeath { get; set; }

    }
}