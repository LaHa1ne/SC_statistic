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
using System.Numerics;
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
                var playersWithRequestedNickname = await _playerRepository.GetPlayersByNicknameWithCorrectInfo(requestedNickname);
                foreach (var playerWithRequestedNickname in playersWithRequestedNickname)
                {
                    await _notificationRepository.Create(new Notification(NotificationType.PlayerChangedNickname, requestedNickname));
                    playerWithRequestedNickname.IsInformationCorrect = false;
                    await _playerRepository.Update(playerWithRequestedNickname);
                }
                return;
            }

            if (response?.Code == 0)
            {
                var playersWithRequestedNickname = await _playerRepository.GetPlayersByNicknameWithCorrectInfo(requestedNickname);
                foreach (var playerWithRequestedNickname in playersWithRequestedNickname)
                {
                    if (playerWithRequestedNickname.PlayerId != response.Stat!.Uid)
                    {
                        await _notificationRepository.Create(new Notification(NotificationType.PlayerChangedNickname, requestedNickname));
                        playerWithRequestedNickname.IsInformationCorrect = false;
                        await _playerRepository.Update(playerWithRequestedNickname);
                    }
                }

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
                        player.IsInformationCorrect = true;
                        await _playerRepository.Update(player);
                    }

                    if (corporation == null && player.CurrentCorporation != null || corporation != null && player.CurrentCorporation == null || corporation?.CorporationId != player.CurrentCorporationId)
                    {
                        await _notificationRepository.Create(new Notification(NotificationType.PlayerChangedCorporation, player.CurrentNickname,
                            player.CurrentCorporation == null ? "" : $"{player.CurrentCorporation.CurrentName}[{player.CurrentCorporation.CurrentTag}]",
                            corporation == null ? "" : $"{corporation.CurrentName}[{corporation.CurrentTag}]"));
                        player.CorporationHistory.Add(new PlayerCorporationHistory(player.PlayerId, corporation, DateTime.Now));
                        player.CurrentCorporation = corporation;
                        player.IsInformationCorrect = true;
                        await _playerRepository.Update(player);
                    }

                    if (!player.IsInformationCorrect)
                    {
                        player.IsInformationCorrect = true;
                        await _playerRepository.Update(player);
                    }
                }
                return;
            }
        }

        public async Task UpdateInformation(ResponseWithStat response, Player updatedPlayer)
        {
            if (response?.Code == 1 || updatedPlayer.PlayerId != response!.Stat?.Uid)
            {
                if (updatedPlayer.IsInformationCorrect)
                {
                    await _notificationRepository.Create(new Notification(NotificationType.PlayerChangedNickname, updatedPlayer.CurrentNickname));
                    updatedPlayer.IsInformationCorrect = false;
                    await _playerRepository.Update(updatedPlayer);
                }
                return;
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


                if (response.Stat.NickName != updatedPlayer.CurrentNickname)
                {
                    await _notificationRepository.Create(new Notification(NotificationType.PlayerChangedNickname, updatedPlayer.CurrentNickname, response.Stat.NickName));
                    updatedPlayer.NicknameHistory.Add(new PlayerNicknameHistory(updatedPlayer.PlayerId, response.Stat.NickName, DateTime.Now));
                    updatedPlayer.CurrentNickname = response.Stat.NickName;
                    updatedPlayer.IsInformationCorrect = true;
                    await _playerRepository.Update(updatedPlayer);
                }

                if (corporation == null && updatedPlayer.CurrentCorporation != null || corporation != null && updatedPlayer.CurrentCorporation == null || corporation?.CorporationId != updatedPlayer.CurrentCorporationId)
                {
                    await _notificationRepository.Create(new Notification(NotificationType.PlayerChangedCorporation, updatedPlayer.CurrentNickname,
                        updatedPlayer.CurrentCorporation == null ? "" : $"{updatedPlayer.CurrentCorporation.CurrentName}[{updatedPlayer.CurrentCorporation.CurrentTag}]",
                        corporation == null ? "" : $"{corporation.CurrentName}[{corporation.CurrentTag}]"));
                    updatedPlayer.CorporationHistory.Add(new PlayerCorporationHistory(updatedPlayer.PlayerId, corporation, DateTime.Now));
                    updatedPlayer.CurrentCorporation = corporation;
                    updatedPlayer.IsInformationCorrect = true;
                    await _playerRepository.Update(updatedPlayer);
                }

                if (!updatedPlayer.IsInformationCorrect)
                {
                    updatedPlayer.IsInformationCorrect = true;
                    await _playerRepository.Update(updatedPlayer);
                }
                return;
            }
        }
    }
}


