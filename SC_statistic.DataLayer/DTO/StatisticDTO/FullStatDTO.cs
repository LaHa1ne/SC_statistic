using Newtonsoft.Json;
using SC_statistic.DataLayer.DTO.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.StatisticDTO
{
    public class FullStatDTO
    {
        public int Uid { get; set; }
        public string Nickname { get; set; } = null!;
        public string Corporation { get; set; } = null!;
        public int AccountRank { get; set; }
        public int PrestigeBonus { get; set; }
        public int Karma { get; set; }
        public PvpStatDTO PvpStat { get; set; } = null!;
        public PvpEffDTO PvpEff { get; set; } = null!;
        public PveStatDTO PveStat { get; set; } = null!;
        public CoopStatDTO CoopStat { get; set; } = null!;
        public UnlimPveMissionLevelsDTO UnlimPveMissionLevels { get; set; } = null!;
    }
}
