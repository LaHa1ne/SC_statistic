using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Entities
{
    public class Player
    {
        public long PlayerId { get; set; }
        public string CurrentNickname { get; set; } = null!;
        public bool IsInformationCorrect { get; set; }
        public long? CurrentCorporationId { get; set; }

        public List<PlayerNicknameHistory> NicknameHistory { get; set; } = new List<PlayerNicknameHistory>();
        public List<PlayerCorporationHistory> CorporationHistory { get; set; } = new List<PlayerCorporationHistory>();

        protected Corporation? _currentCorporation;
        public Corporation? CurrentCorporation
        {
            /*get; set;*/
            get => _currentCorporation;
            set
            {
                _currentCorporation = value;
                CurrentCorporationId = value == null ? null : value.CorporationId;
            }
        }
        public List<TrackedPlayer> TrackedPlayers { get; set; } = new List<TrackedPlayer>();

    }
}
