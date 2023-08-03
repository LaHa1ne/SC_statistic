using AutoMapper;
using Newtonsoft.Json;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataAccessLayer.Repositories;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Enums;
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
    public class InformationService : IInformationService
    {
        protected readonly IMapper _mapper;
        protected readonly IPlayerRepository _playerRepository;
        protected readonly ICorporationRepository _corporationRepository;
        protected readonly INotificationRepository _notificationRepository;

        public InformationService(IMapper mapper, IPlayerRepository playerRepository, ICorporationRepository corporationRepository, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
            _corporationRepository = corporationRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task UpdateInformation(ResponseWithStat response, string requestedNickname)
        {
            if (response?.Code == 1)
            {
                var player = await _playerRepository.GetByNickname(requestedNickname);
                if (player != null && player.IsInformationCorrect)
                {
                    await _notificationRepository.Create(new Notification(NotificationType.PlayerChangedNickname, requestedNickname));
                    player.IsInformationCorrect = false;
                    await _playerRepository.Update(player);
                }
            }

            if (response?.Code == 0)
            {
                var currentCorporation = response.Stat!.Corp;
                Corporation? corporation = null;
                if (currentCorporation != null)
                {
                    corporation = await _corporationRepository.GetByCorporationId(currentCorporation.Cid);
                    if (corporation == null)
                    {
                        corporation = _mapper.Map<Corporation>(currentCorporation);
                        await _corporationRepository.Create(corporation);
                    }
                    else
                    {
                        if (currentCorporation.Name != corporation.CurrentName || currentCorporation.Tag != corporation.CurrentTag)
                        {
                            await _notificationRepository.Create(new Notification(NotificationType.CorporationChangedName, $"{corporation.CurrentName}[{corporation.CurrentTag}]", $"{currentCorporation.Name}[{currentCorporation.Tag}]"));
                            corporation.CurrentTag = currentCorporation.Tag!;
                            corporation.CurrentName = currentCorporation.Name;
                        }
                        corporation.PvpRating = currentCorporation.PvpRating;
                        corporation.PveRating = currentCorporation.PveRating;
                        await _corporationRepository.Update(corporation);
                    }
                }

                var player = await _playerRepository.GetByPlayerId(response.Stat.Uid);
                if (player == null)
                {
                    player = _mapper.Map<Player>(response.Stat);
                    player.CurrentCorporation = corporation;
                    player.NicknameHistory.Add(new PlayerNicknameHistory(player.PlayerId, player.CurrentNickname, DateTime.Now));
                    player.CorporationHistory.Add(new PlayerCorporationHistory(player.PlayerId, corporation, DateTime.Now));
                    await _playerRepository.Create(player);
                }
                else
                {
                    if (response.Stat.NickName != player.CurrentNickname)
                    {
                        await _notificationRepository.Create(new Notification(NotificationType.PlayerChangedNickname, player.CurrentNickname, response.Stat.NickName));
                        player.NicknameHistory.Add(new PlayerNicknameHistory(player.PlayerId, response.Stat.NickName, DateTime.Now));
                        player.CurrentNickname = response.Stat.NickName;
                        await _playerRepository.Update(player);
                    }

                    if (corporation == null && player.CurrentCorporation != null || corporation != null && player.CurrentCorporation == null || corporation!.CorporationId != player.CurrentCorporationId)
                    {
                        await _notificationRepository.Create(new Notification(NotificationType.PlayerChangedCorporation, player.CurrentNickname,
                            player.CurrentCorporation == null ? "" : $"{player.CurrentCorporation.CurrentName}+[{player.CurrentCorporation.CurrentTag}]",
                            corporation == null ? "" : $"{corporation.CurrentName}+[{corporation.CurrentTag}]"));
                        player.CorporationHistory.Add(new PlayerCorporationHistory(player.PlayerId, corporation, DateTime.Now));
                        player.CurrentCorporation = corporation;
                        await _playerRepository.Update(player);
                    }
                }
            }
        }
    }
}


