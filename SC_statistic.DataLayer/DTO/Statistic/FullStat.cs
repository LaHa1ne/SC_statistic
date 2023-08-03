using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Statistic
{
    public class FullStat
    {
        public double EffRating { get; set; }
        public string NickName { get; set; } = null!;
        public double PrestigeBonus { get; set; }
        public int Uid { get; set; }
        public int AccountRank { get; set; }
        public PveStat Pve { get; set; } = null!;
        public PvpStat Pvp { get; set; } = null!;
        public CoopStat Coop { get; set; } = null!;
        public OpenWorldStat OpenWorld { get; set; } = null!;

        [JsonProperty("clan")]
        public CorpStat Corp { get; set; } = null!;

    }
}
