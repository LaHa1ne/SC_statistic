using Microsoft.EntityFrameworkCore;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(ApplicationDbContext db) : base(db)
        {
        }
        public async Task<Player> GetByPlayerId(long playerId)
        {
            return await _db.Players.Include(p => p.CurrentCorporation).Include(p => p.NicknameHistory).Include(p => p.CorporationHistory).ThenInclude(ch=>ch.Corporation).FirstOrDefaultAsync(p => p.PlayerId == playerId);
        }

        public async Task<Player> GetByNickname(string nickname)
        {
            var player = await _db.Players.Include(p => p.CurrentCorporation).Include(p => p.NicknameHistory).Include(p => p.CorporationHistory).FirstOrDefaultAsync(p => p.CurrentNickname == nickname && p.IsInformationCorrect);
            return player != null ? player : await _db.Players.Include(p => p.CurrentCorporation).Include(p => p.NicknameHistory).Include(p => p.CorporationHistory).FirstOrDefaultAsync(p => p.CurrentNickname == nickname && !p.IsInformationCorrect);
        }
    }
}
