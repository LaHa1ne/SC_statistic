using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.StatisticDTO
{
    public class PvpStatDTO
    {
        public int GamePlayed { get; set; }
        public double WinCoef { get; set; }
        public double TotalVpDmgDone { get; set; }
        public string TotalBattleTime { get; set; } = null!;
        public double EffRating { get; set; }
    }
}