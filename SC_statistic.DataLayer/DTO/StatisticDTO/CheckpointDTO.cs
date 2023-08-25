using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.DTO.StatisticDTO
{
    public class CheckpointDTO
    {
        public Guid CheckpointId { get; set; }
        public string Name { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string Time { get; set; } = null!;
        public CheckpointStatDTO CheckpointStat { get; set; } = null!;

        public CheckpointDTO() { }
        public CheckpointDTO(Checkpoint checkpoint)
        {
            CheckpointId = checkpoint.CheckpointId;
            Name = checkpoint.Name;
            Date = checkpoint.Date.ToString("dd.MM.yyyy");
            Time = checkpoint.Date.ToString("HH:mm");
            CheckpointStat = new CheckpointStatDTO(checkpoint.CheckpointStat);
        }

        public CheckpointDTO(Checkpoint startCheckpoint, Checkpoint endCheckpoint)
        {
            CheckpointId = endCheckpoint.CheckpointId;
            Name = endCheckpoint.Name;
            Date = endCheckpoint.Date.ToString("dd.MM.yyyy");
            Time = endCheckpoint.Date.ToString("HH:mm");
            CheckpointStat = new CheckpointStatDTO(startCheckpoint.CheckpointStat, endCheckpoint.CheckpointStat);
        }

    }
}
