using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Statistic
{
    public class PvpStat
    {
        public int GamePlayed { get; set; }
        public int GameWin { get; set; }
        public int TotalAssists { get; set; }
        public long TotalBattleTime { get; set; }
        public int TotalDeath { get; set; }
        public double TotalDmgDone { get; set; }
        public double TotalHealingDone { get; set; }
        public int TotalKill { get; set; }
        public double TotalVpDmgDone { get; set; }
    }
}
