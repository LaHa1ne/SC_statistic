using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.Corporation
{
    public class PlayerInCorporationDTO
    {
        public long PlayerId { get; set; }
        public string CurrentNickname { get; set; } = null!;
        public bool IsInformationCorrect { get; set; }
    }
}
