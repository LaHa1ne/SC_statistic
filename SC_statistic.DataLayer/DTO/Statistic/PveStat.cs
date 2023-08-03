using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Statistic
{
    public class PveStat
    {
        public int GamePlayed { get; set; }

        [JsonProperty("unlimPve_playerAttackLevel")]
        public int UnlimPvePlayerAttackLevel { get; set; }

        [JsonProperty("unlimPve_playerDefenceLevel")]
        public int UnlimPvePlayerDefenceLevel { get; set; }

        [JsonProperty("unlimPve_missionLevels")]
        public UnlimPveMissionLevels PveMissionLevels { get; set; } = null!;

        [JsonProperty("wavePve_maxWave")]
        public int WavePveMaxWave { get; set; }
    }
}
