using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Entities
{
    public class Corporation
    {
        public long CorporationId { get; set; }
        public string CurrentTag { get; set; } = null!;
        public string CurrentName { get; set; } = null!;
        public int PvpRating { get; set; }
        public int PveRating { get; set; }


        public List<Player> Players { get; set; } = new List<Player>();
        public List<PlayerCorporationHistory> CorporationHistories { get; set; } = new List<PlayerCorporationHistory>();
    }
}
