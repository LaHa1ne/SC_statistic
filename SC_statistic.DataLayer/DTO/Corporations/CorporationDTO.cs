using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Corporation
{
    public class CorporationDTO
    {
        public string CurrentTag { get; set; } = null!;
        public string CurrentName { get; set; } = null!;
        public int PvpRating { get; set; }
        public int PveRating { get; set; }
        public List<PlayerInCorporationDTO> Players { get; set; } = null!;
    }
}
