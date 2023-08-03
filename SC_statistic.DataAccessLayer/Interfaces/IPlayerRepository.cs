using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.Interfaces
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        Task<Player> GetByPlayerId(long playerId);
        Task<Player> GetByNickname(string nickname);
    }
}
