using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.Interfaces
{
    public interface ITrackedPlayerRepository : IBaseRepository<TrackedPlayer>
    {
        Task<TrackedPlayer> GetByTrackedPlayerId(Guid trackedPlayerId);
        Task<TrackedPlayer> GetByUserIdAndPlayerId(Guid userId, long playerId);

    }
}