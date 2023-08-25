using AutoMapper;
using SC_statistic.DataLayer.DTO.Corporation;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.DTO.StatisticDTO;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Mappers.PlayerStatistic
{
    public class FullStatToFullStatDTOMapperProfile : Profile
    {
        public FullStatToFullStatDTOMapperProfile()
        {
            CreateMap<FullStat, FullStatDTO>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.NickName))
                .ForMember(dest => dest.Corporation, opt => opt.MapFrom(src => src.Corp == null ? "Без корпорации" : $"{src.Corp.Name}[{src.Corp.Tag}]"))
                .ForMember(dest => dest.AccountRank, opt => opt.MapFrom(src => src.AccountRank))
                .ForMember(dest => dest.PrestigeBonus, opt => opt.MapFrom(src => (int)(src.PrestigeBonus * 100)))
                .ForMember(dest => dest.Karma, opt => opt.MapFrom(src => src.OpenWorld.Karma))
                .ForMember(dest => dest.PvpStat, opt => opt.MapFrom(src => new PvpStatDTO()
                {
                    GamePlayed = src.Pvp.GamePlayed,
                    WinCoef = src.Pvp.GamePlayed == 0 ? 0 : Math.Round((double)src.Pvp.GameWin / (src.Pvp.GamePlayed - src.Pvp.GameWin), 2),
                    TotalVpDmgDone = (long)src.Pvp.TotalVpDmgDone,
                    TotalBattleTime = ConvertMilisecondsToTimeRange.Convert(src.Pvp.TotalBattleTime),
                    EffRating = (int)src.EffRating
                }))
                .ForMember(dest => dest.PvpEff, opt => opt.MapFrom(src => new PvpEffDTO()
                {
                    KillsPerBattle = src.Pvp.GamePlayed == 0 ? 0 : Math.Round((double)src.Pvp.TotalKill / src.Pvp.GamePlayed, 2),
                    AssistPerBattle = src.Pvp.GamePlayed == 0 ? 0 : Math.Round((double)src.Pvp.TotalAssists / src.Pvp.GamePlayed, 2),
                    kd = src.Pvp.TotalDeath == 0 ? 0 : Math.Round((double)src.Pvp.TotalKill / src.Pvp.TotalDeath, 2),
                    ad = src.Pvp.TotalDeath == 0 ? 0 : Math.Round((double)src.Pvp.TotalAssists / src.Pvp.TotalDeath, 2),
                    DeathsPerBattle = src.Pvp.GamePlayed == 0 ? 0 : Math.Round((double)src.Pvp.TotalDeath / src.Pvp.GamePlayed, 2),
                    ecm_rating = 1.0  //Заглушка
                }))
                .ForMember(dest => dest.PveStat, opt => opt.MapFrom(src => new PveStatDTO()
                {
                    GamePlayed = src.Pve.GamePlayed,
                    UnlimPvePlayerAttackLevel = src.Pve.UnlimPvePlayerAttackLevel,
                    UnlimPvePlayerDefenceLevel = src.Pve.UnlimPvePlayerDefenceLevel
                }))
                .ForMember(dest => dest.CoopStat, opt => opt.MapFrom(src => new CoopStatDTO()
                {
                    GamePlayed = src.Coop.GamePlayed,
                    GameWin = src.Coop.GameWin,
                    TotalBattleTime = ConvertMilisecondsToTimeRange.Convert(src.Coop.TotalBattleTime),
                }))
                .ForMember(dest => dest.UnlimPveMissionLevels, opt => opt.MapFrom(src => src.Pve.PveMissionLevels == null ? new UnlimPveMissionLevelsDTO() : new UnlimPveMissionLevelsDTO()
                {
                    PveFrozenStation = src.Pve.PveMissionLevels.PveFrozenStation,
                    CaptureRepairbase = src.Pve.PveMissionLevels.CaptureRepairbase,
                    PlanetWarWaves = src.Pve.PveMissionLevels.PlanetWarWaves,
                    BigshipBuildingEasy = src.Pve.PveMissionLevels.BigshipBuildingEasy,
                    PveEmpfrontierWaves = src.Pve.PveMissionLevels.PveEmpfrontierWaves,
                    PveJerichoBase = src.Pve.PveMissionLevels.PveJerichoBase,
                    BigshipBuilding2Easy = src.Pve.PveMissionLevels.BigshipBuilding2Easy,
                    NalnifanLumenWaves = src.Pve.PveMissionLevels.NalnifanLumenWaves,
                    LootGeostationNormal = src.Pve.PveMissionLevels.LootGeostationNormal,
                    PveDesttownWavesEasy = src.Pve.PveMissionLevels.PveDesttownWavesEasy,
                    AsteroidBuilding = src.Pve.PveMissionLevels.AsteroidBuilding,
                    PiratebayHard = src.Pve.PveMissionLevels.PiratebayHard,
                    RescuePiratesBase = src.Pve.PveMissionLevels.RescuePiratesBase,
                    MagnificentSeven = src.Pve.PveMissionLevels.MagnificentSeven,
                    Stealth = src.Pve.PveMissionLevels.Stealth,
                    WavePveMaxWave = src.Pve.WavePveMaxWave
                }));
        }
    }
}