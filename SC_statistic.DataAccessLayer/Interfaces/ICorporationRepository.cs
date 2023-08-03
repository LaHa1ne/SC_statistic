using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.Interfaces
{
    public interface ICorporationRepository : IBaseRepository<Corporation>
    {
        Task<Corporation> GetByName(string name);
        Task<Corporation> GetByTag(string tag);
        Task<Corporation> GetByCorporationId(long corporationId);
    }
}
