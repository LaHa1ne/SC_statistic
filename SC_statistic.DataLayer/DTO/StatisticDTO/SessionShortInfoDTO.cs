using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.StatisticDTO
{
    public class SessionShortInfoDTO
    {
        public Guid SessionId { get; set; }
        public string Name { get; set; } = null!;
        public string StartDate { get; set; } = null!;

    }
}
