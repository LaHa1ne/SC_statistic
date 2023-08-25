using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.StatisticDTO
{
    public class CheckpointStatDTO
    {
        public int GamePlayed { get; set; }
        public double WinCoef { get; set; }
        public double KillsPerBattle { get; set; }
        public double AssistPerBattle { get; set; }
        public double kd { get; set; }
        public double ad { get; set; }
        public double DeathsPerBattle { get; set; }

        public CheckpointStatDTO() { }
        public CheckpointStatDTO(CheckpointStat checkpointStat)
        {
            GamePlayed = checkpointStat.GamePlayed;
            WinCoef = Math.Round((double)checkpointStat.GameWin / (checkpointStat.GamePlayed - checkpointStat.GameWin), 2);
            KillsPerBattle = Math.Round((double)checkpointStat.TotalKill / checkpointStat.GamePlayed, 2);
            AssistPerBattle = Math.Round((double)checkpointStat.TotalAssists / checkpointStat.GamePlayed, 2);
            kd = checkpointStat.TotalDeath == 0 ? 0 : Math.Round((double)checkpointStat.TotalKill / checkpointStat.TotalDeath, 2);
            ad = checkpointStat.TotalDeath == 0 ? 0 : Math.Round((double)checkpointStat.TotalAssists / checkpointStat.TotalDeath, 2);
            DeathsPerBattle = Math.Round((double)checkpointStat.TotalDeath / checkpointStat.GamePlayed, 2);
        }

        public CheckpointStatDTO(CheckpointStat startCheckpointStat, CheckpointStat endCheckpointStat)
        {
            if (startCheckpointStat.GamePlayed == endCheckpointStat.GamePlayed)
            {
                GamePlayed = 0;
                WinCoef = 0;
                KillsPerBattle = 0;
                AssistPerBattle = 0;
                kd = 0;
                ad = 0;
                DeathsPerBattle = 0;
            }
            else
            {
                var GameWin = endCheckpointStat.GameWin - startCheckpointStat.GameWin;
                var TotalKill = endCheckpointStat.TotalKill - startCheckpointStat.TotalKill;
                var TotalAssists = endCheckpointStat.TotalAssists - startCheckpointStat.TotalAssists;
                var TotalDeath = endCheckpointStat.TotalDeath - startCheckpointStat.TotalDeath;
                GamePlayed = endCheckpointStat.GamePlayed - startCheckpointStat.GamePlayed;
                WinCoef = Math.Round((double)GameWin / (GamePlayed - GameWin), 2);
                KillsPerBattle = Math.Round((double)TotalKill / GamePlayed, 2);
                AssistPerBattle = Math.Round((double)TotalAssists / GamePlayed, 2);
                kd = TotalDeath == 0 ? 0 : Math.Round((double)TotalKill / TotalDeath, 2);
                ad = TotalDeath == 0 ? 0 : Math.Round((double)TotalAssists / TotalDeath, 2);
                DeathsPerBattle = Math.Round((double)TotalDeath / GamePlayed, 2);
            }
        }

    }
}
