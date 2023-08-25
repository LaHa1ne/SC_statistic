using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.StatisticDTO
{
    public class TrackedPlayerShortInfoDTO
    {
        public Guid TrackedPlayerId { get; set; }
        public string Nickname { get; set; } = null!;
    }
}
