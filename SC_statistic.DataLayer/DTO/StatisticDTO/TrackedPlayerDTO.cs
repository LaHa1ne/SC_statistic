using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.StatisticDTO
{
    public class TrackedPlayerDTO
    {
        public Guid TrackedPlayerId { get; set; }
        public FullStatDTO Stat { get; set; } = null!;
        public List<SessionShortInfoDTO> Sessions { get; set; } = null!;
    }
}
