using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataAccessLayer.Repositories;
using SC_statistic.DataAccessLayer;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC_statistic.DataLayer.DTO.Statistic;
using Microsoft.EntityFrameworkCore;

namespace SC_statistic.DataAccessLayer.Repositories
{
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Session> GetBySessionId(Guid sessionId)
        {
            return await _db.Sessions.Include(s => s.Checkpoints).ThenInclude(c => c.CheckpointStat).FirstOrDefaultAsync(s =>s.SessionId == sessionId);
        }

        public async Task<Session> GetBySessionIdOnlyFirstAndLastCheckpoints(Guid sessionId)
        {
            var minDate = await _db.Checkpoints.Where(c => c.SessionId == sessionId).MinAsync(c => c.Date);
            var maxDate = await _db.Checkpoints.Where(c => c.SessionId == sessionId).MaxAsync(c => c.Date);
            return await _db.Sessions.Include(s => s.Checkpoints.Where(c => c.Date == minDate || c.Date == maxDate).OrderBy(c => c.Date)).FirstOrDefaultAsync(s => s.SessionId == sessionId);
        }
    }
}