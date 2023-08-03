using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Statistic
{
    public class ResponseWithStat
    {
        public string Result { get; set; } = null!;
        public int Code { get; set; }

        [JsonProperty("data")]
        public FullStat? Stat { get; set; } 
    }
}
