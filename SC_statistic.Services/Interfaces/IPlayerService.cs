using SC_statistic.DataLayer.DTO.Notifications;
using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<BaseResponse<PlayerHistoryDTO>> GetPlayerHistory(string nickname);
    }
}
