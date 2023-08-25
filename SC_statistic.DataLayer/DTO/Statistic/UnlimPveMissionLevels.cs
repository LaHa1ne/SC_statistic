using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Statistic
{
    public class UnlimPveMissionLevels
    {
        [JsonProperty("pve_frozen_station_t2")]
        public int PveFrozenStation { get; set; }

        [JsonProperty("capture_repairbase_t1")]
        public int CaptureRepairbase { get; set; }

        [JsonProperty("planet_war_waves_T1")]
        public int PlanetWarWaves { get; set; }

        [JsonProperty("bigship_building_easy")]
        public int BigshipBuildingEasy { get; set; }

        [JsonProperty("pve_empfrontier_waves_T1")]
        public int PveEmpfrontierWaves { get; set; }

        [JsonProperty("pve_jericho_base_t2")]
        public int PveJerichoBase { get; set; }

        [JsonProperty("bigship_building_2_easy")]
        public int BigshipBuilding2Easy { get; set; }

        [JsonProperty("nalnifan_lumen_waves_T1")]
        public int NalnifanLumenWaves { get; set; }

        [JsonProperty("loot_geostation_normal")]
        public int LootGeostationNormal { get; set; }

        [JsonProperty("pve_desttown_waves_easy")]
        public int PveDesttownWavesEasy { get; set; }

        [JsonProperty("asteroid_building_t1")]
        public int AsteroidBuilding { get; set; }

        [JsonProperty("piratebay_hard")]
        public int PiratebayHard { get; set; }

        [JsonProperty("rescue_pirates_base")]
        public int RescuePiratesBase { get; set; }

        [JsonProperty("magnificent_seven")]
        public int MagnificentSeven { get; set; }

        [JsonProperty("stealth")]
        public int Stealth { get; set; }
    }
}