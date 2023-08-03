using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.PlayerHistory
{
    public class PlayerHistoryDTO
    {
        public long PlayerId { get; set; }
        public string CurrentNickname { get; set; } = null!;
        public bool IsInformationCorrect { get; set; }
        public string CurrentCorpName { get; set; } = null!;

        public List<PlayerNicknameHistoryDTO> NicknameHistory { get; set; } = null!;
        public List<PlayerCorporationHistoryDTO> CorporationHistory { get; set; } = null!;
    }
}
