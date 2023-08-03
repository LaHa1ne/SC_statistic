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
    public class CorporationRepository : BaseRepository<Corporation>, ICorporationRepository
    {
        public CorporationRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Corporation> GetByName(string name)
        {
            return await _db.Corporations.Include(c => c.Players).FirstOrDefaultAsync(c => c.CurrentName == name);
        }

        public async Task<Corporation> GetByTag(string tag)
        {
            return await _db.Corporations.Include(c => c.Players).FirstOrDefaultAsync(c => c.CurrentTag == tag);
        }

        public async Task<Corporation> GetByCorporationId(long corporationId)
        {
            return await _db.Corporations.FirstOrDefaultAsync(c => c.CorporationId == corporationId);
        }

    }
}
