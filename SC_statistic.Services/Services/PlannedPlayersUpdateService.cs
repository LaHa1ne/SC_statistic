using AutoMapper;
using Newtonsoft.Json;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Responses;
using SC_statistic.Services.Interfaces;
using SC_statistic.Services.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Services
{
    public class PlannedPlayersUpdateService : InformationService, IPlannedPlayersUpdateService
    {
        protected const int pageSize = 20;
        protected readonly ISC_ApiService _SC_ApiService;

        public PlannedPlayersUpdateService(IMapper mapper, IPlayerRepository playerRepository, ICorporationRepository corporationRepository, INotificationRepository notificationRepository, ISC_ApiService SC_ApiService)
            : base(mapper, playerRepository, corporationRepository, notificationRepository)
        {
            _SC_ApiService = SC_ApiService;
        }

        public async Task UpdateAllPlayers()
        {
            try
            {
                int currentPage = 1;
                while (true)
                {
                    var players = await _playerRepository.GetPlayersOnSelectedPage(currentPage, pageSize);
                    foreach (var player in players)
                    {
                        var response = await _SC_ApiService.GetPlayerInformation(player.CurrentNickname);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = JsonConvert.DeserializeObject<ResponseWithStat>(await response.Content.ReadAsStringAsync());
                            await UpdateInformation(content!, player);
                        }
                        else throw new Exception("Сервер SC недоступен");
                    }
                    if (players.Count < pageSize) break;
                    currentPage++;
                }
            }
            catch(Exception ex)
            {
                //Затычка
                //Ошибка при обновлении данных
            }
        }
    }
}