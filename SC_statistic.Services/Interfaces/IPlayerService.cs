using SC_statistic.DataLayer.DTO.Notifications;
using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.DTO.StatisticDTO;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Responses;
using SC_statistic.DataLayer.ViewModels.PlayerStatistic;
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
        Task<BaseResponse<TrackedPlayerListDTO>> GetTrackedPlayers(Guid userId);
        Task<BaseResponse<TrackedPlayerDTO>> GetTrackedPlayerStatistic(Guid userId, string nickname);
        Task<BaseResponse<SessionDTO>> GetSession(Guid userId, SessionViewModel sessionViewModel);
        Task<BaseResponse<AddCheckpointDTO>> AddCheckpoint(Guid userId, CheckpointViewModel checkpointViewModel);
    }
}
