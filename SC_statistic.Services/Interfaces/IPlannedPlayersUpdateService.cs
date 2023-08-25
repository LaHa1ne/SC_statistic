using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Interfaces
{
    public interface IPlannedPlayersUpdateService
    {
        Task UpdateAllPlayers();
    }
}
