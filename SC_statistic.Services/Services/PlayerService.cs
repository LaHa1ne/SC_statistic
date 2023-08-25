using AutoMapper;
using Newtonsoft.Json;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataAccessLayer.Repositories;
using SC_statistic.DataLayer.DTO.Notifications;
using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.DTO.StatisticDTO;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Responses;
using SC_statistic.DataLayer.ViewModels.PlayerStatistic;
using SC_statistic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Services
{
    public class PlayerService : InformationService, IPlayerService
    {
        protected readonly ITrackedPlayerRepository _trackedPlayerRepository;
        protected readonly ISessionRepository _sessionRepository;
        protected readonly IUserRepository _userRepository;
        protected readonly ISC_ApiService _SC_ApiService;

        public PlayerService(IMapper mapper, IPlayerRepository playerRepository, ICorporationRepository corporationRepository, INotificationRepository notificationRepository, ITrackedPlayerRepository trackedPlayerRepository, ISessionRepository sessionRepository, IUserRepository userRepository, ISC_ApiService SC_ApiService)
            : base(mapper, playerRepository, corporationRepository, notificationRepository)
        {
            _trackedPlayerRepository = trackedPlayerRepository;
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
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

        public async Task<BaseResponse<TrackedPlayerListDTO>> GetTrackedPlayers(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetByUserIdWithTrackedPlayers(userId);

                return new BaseResponse<TrackedPlayerListDTO>()
                {
                    Description = "Информация получена",
                    Data = _mapper.Map<TrackedPlayerListDTO>(user),
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TrackedPlayerListDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<TrackedPlayerDTO>> GetTrackedPlayerStatistic(Guid userId, string nickname)
        {
            try
            {
                var response = await _SC_ApiService.GetPlayerInformation(nickname);
                if (response.IsSuccessStatusCode)
                {
                    var content = JsonConvert.DeserializeObject<ResponseWithStat>(await response.Content.ReadAsStringAsync());
                    await UpdateInformation(content!, nickname);

                    var player = await _playerRepository.GetByNickname(nickname);
                    if (player == null)
                    {
                        return new BaseResponse<TrackedPlayerDTO>()
                        {
                            Description = "Игрок не найден",
                            StatusCode = HttpStatusCode.NotFound,
                        };
                    }
                    if (!player.IsInformationCorrect)
                    {
                        return new BaseResponse<TrackedPlayerDTO>()
                        {
                            Description = "Устаревший никнейм",
                            StatusCode = HttpStatusCode.UnprocessableEntity,
                        };
                    }

                    var trackedPlayer = await _trackedPlayerRepository.GetByUserIdAndPlayerId(userId, player.PlayerId);
                    if (trackedPlayer == null)
                    {
                        trackedPlayer = new TrackedPlayer()
                        {
                            UserId = userId,
                            PlayerId = player.PlayerId,
                        };
                        await _trackedPlayerRepository.Create(trackedPlayer);
                    }

                    return new BaseResponse<TrackedPlayerDTO>()
                    {
                        Description = "Информация получена",
                        Data = new TrackedPlayerDTO()
                        {
                            TrackedPlayerId = trackedPlayer.TrackedPlayerId,
                            Stat = _mapper.Map<FullStatDTO>(content!.Stat),
                            Sessions = _mapper.Map<List<SessionShortInfoDTO>>(trackedPlayer.Sessions)
                        },
                        StatusCode = HttpStatusCode.OK
                    };
                }
                throw new Exception("Сервер SC недоступен");
            }
            catch (Exception ex)
            {
                return new BaseResponse<TrackedPlayerDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<SessionDTO>> GetSession(Guid userId, SessionViewModel sessionViewModel)
        {
            try
            {
                if (sessionViewModel.SessionId == null)
                {
                    var trackedPlayer = await _trackedPlayerRepository.GetByTrackedPlayerId(sessionViewModel.TrackedPlayerId);
                    var response = await _SC_ApiService.GetPlayerInformation(trackedPlayer.Player.CurrentNickname);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = JsonConvert.DeserializeObject<ResponseWithStat>(await response.Content.ReadAsStringAsync());

                        if (!trackedPlayer.Player.IsInformationCorrect)
                        {
                            return new BaseResponse<SessionDTO>()
                            {
                                Description = "Устаревший никнейм",
                                StatusCode = HttpStatusCode.UnprocessableEntity,
                            };
                        }

                        var newSession = new Session()
                        {
                            Name = sessionViewModel.SessionName,
                            StartDate = DateTime.Now,
                            TrackedPlayerId = sessionViewModel.TrackedPlayerId
                        };

                        var newCheckpoint = new Checkpoint()
                        {
                            Name = "Стартовый",
                            Date = DateTime.Now,
                            IsStarted = true,
                            CheckpointStat = _mapper.Map<CheckpointStat>(content!.Stat)
                        };

                        newSession.Checkpoints.Add(newCheckpoint);
                        await _sessionRepository.Create(newSession);

                        return new BaseResponse<SessionDTO>()
                        {
                            Description = "Информация получена",
                            Data = _mapper.Map<SessionDTO>(newSession),
                            StatusCode = HttpStatusCode.OK,
                        };
                    }
                    throw new Exception("Сервер SC недоступен");
                }
                else
                {
                    var session = await _sessionRepository.GetBySessionId(sessionViewModel.SessionId.Value);
                    return new BaseResponse<SessionDTO>()
                    {
                        Description = "Информация получена",
                        Data = _mapper.Map<SessionDTO>(session),
                        StatusCode = HttpStatusCode.OK,
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<SessionDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<AddCheckpointDTO>> AddCheckpoint(Guid userId, CheckpointViewModel checkpointViewModel)
        {
            try
            {
                var trackedPlayer = await _trackedPlayerRepository.GetByTrackedPlayerId(checkpointViewModel.TrackedPlayerId);
                var response = await _SC_ApiService.GetPlayerInformation(trackedPlayer.Player.CurrentNickname);
                if (response.IsSuccessStatusCode)
                {
                    var content = JsonConvert.DeserializeObject<ResponseWithStat>(await response.Content.ReadAsStringAsync());

                    if (!trackedPlayer.Player.IsInformationCorrect)
                    {
                        return new BaseResponse<AddCheckpointDTO>()
                        {
                            Description = "Устаревший никнейм",
                            StatusCode = HttpStatusCode.NotFound,
                        };
                    }

                    var session = await _sessionRepository.GetBySessionIdOnlyFirstAndLastCheckpoints(checkpointViewModel.SessionId);

                    var newCheckpoint = new Checkpoint()
                    {
                        Name = checkpointViewModel.CheckpointName,
                        Date = DateTime.Now,
                        IsStarted = false,
                        CheckpointStat = _mapper.Map<CheckpointStat>(content!.Stat)
                    };

                    if (session.Checkpoints.Last().CheckpointStat.GamePlayed == newCheckpoint.CheckpointStat.GamePlayed)
                    {
                        return new BaseResponse<AddCheckpointDTO>()
                        {
                            Description = "С момента последнего чекпоинта не сыграно ни одного боя",
                            StatusCode = HttpStatusCode.UnprocessableEntity,
                        };
                    }

                    session.Checkpoints.Add(newCheckpoint);
                    await _sessionRepository.Update(session);

                    return new BaseResponse<AddCheckpointDTO>()
                    {
                        Description = "Чекпоинт добавлен",
                        Data = _mapper.Map<AddCheckpointDTO>(session),
                        StatusCode = HttpStatusCode.OK,
                    };
                }

                throw new Exception("Сервер SC недоступен");
            }

            catch (Exception ex)
            {
                return new BaseResponse<AddCheckpointDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

    }
}