using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataAccessLayer.Repositories;
using SC_statistic.DataAccessLayer;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SC_statistic.DataAccessLayer.Repositories
{
    public class TrackedPlayerRepository : BaseRepository<TrackedPlayer>, ITrackedPlayerRepository
    {
        public TrackedPlayerRepository(ApplicationDbContext db) : base(db)
        {
        }
        public async Task<TrackedPlayer> GetByTrackedPlayerId(Guid trackedPlayerId)
        {
            return await _db.TrackedPlayers.Include(tp => tp.Player).FirstOrDefaultAsync(tp => tp.TrackedPlayerId == trackedPlayerId);
        }
        public async Task<TrackedPlayer> GetByUserIdAndPlayerId(Guid userId, long playerId)
        {
            return await _db.TrackedPlayers.Include(tp => tp.Player).Include(tp => tp.Sessions.OrderBy(s => s.StartDate)).FirstOrDefaultAsync(tp => tp.UserId == userId && tp.PlayerId == playerId);
        }
    }
}