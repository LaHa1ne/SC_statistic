using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Statistic
{
    public class CorpStat
    {
        public string Name { get; set; } = null!;
        public string? Tag { get; set; }
        public int Cid { get; set; }
        public int PvpRating { get; set; }
        public int PveRating { get; set; }
    }
}
