using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.StatisticDTO
{
    public class PveStatDTO
    {
        public int GamePlayed { get; set; }
        public int UnlimPvePlayerAttackLevel { get; set; }
        public int UnlimPvePlayerDefenceLevel { get; set; }
    }
}
