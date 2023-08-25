using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.Interfaces
{
    public interface ISessionRepository : IBaseRepository<Session>
    {
        Task<Session> GetBySessionId(Guid sessionId);
        Task<Session> GetBySessionIdOnlyFirstAndLastCheckpoints(Guid sessionId);
    }
}