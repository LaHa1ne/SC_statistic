using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Entities
{
    public class PlayerCorporationHistory
    {
        public long PlayerCorporationHistoryId { get; set; }
        public long PlayerId { get; set; }
        public long? CorporationId { get; set; }
        public DateTime Date { get; set; }

        public Player Player { get; set; } = null!;
        public Corporation? Corporation { get; set; }

        public PlayerCorporationHistory() { }
        public PlayerCorporationHistory(long playerId, Corporation? corporation, DateTime date) 
        {
            PlayerId = playerId;
            Corporation = corporation;
            Date = date;
        }


    }
}
