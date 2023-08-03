using AutoMapper;
using Newtonsoft.Json;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataLayer.DTO.Notifications;
using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Responses;
using SC_statistic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Services
{
    public class PlayerService : InformationService, IPlayerService
    {
        protected readonly ISC_ApiService _SC_ApiService;

        public PlayerService(IMapper mapper, IPlayerRepository playerRepository, ICorporationRepository corporationRepository, INotificationRepository notificationRepository, ISC_ApiService SC_ApiService) 
            : base(mapper, playerRepository, corporationRepository, notificationRepository) 
        {
            _SC_ApiService = SC_ApiService;
        }

        public async Task<BaseResponse<PlayerHistoryDTO>> GetPlayerHistory(string nickname)
        {
            try
            {
                var response = await _SC_ApiService.GetPlayerInformation(nickname);
                if (response.IsSuccessStatusCode)
                {
                    var content = JsonConvert.DeserializeObject<ResponseWithStat>(await response.Content.ReadAsStringAsync());
                    await UpdateInformation(content!, nickname);

                    var player = await _playerRepository.GetByNickname(nickname);
                    if (player != null)
                    {
                        return new BaseResponse<PlayerHistoryDTO>()
                        {
                            Description = "Информация получена",
                            Data = _mapper.Map<PlayerHistoryDTO>(player),
                            StatusCode = HttpStatusCode.OK,
                        };
                    }
                    return new BaseResponse<PlayerHistoryDTO>()
                    {
                        Description = "Игрок с данным никнеймом не найден",
                        StatusCode = HttpStatusCode.NotFound,
                    };
                }
                throw new Exception("Сервер SC недоступен");
            }
            catch (Exception ex) 
            {
                return new BaseResponse<PlayerHistoryDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }


    }
}
