using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.PlayerHistory
{
    public class PlayerNicknameHistoryDTO
    {
        public string Nickname { get; set; } = null!;
        public string Date { get; set; } = null!;

    }
}
