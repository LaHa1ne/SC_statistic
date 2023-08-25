using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.ViewModels.PlayerStatistic
{
    public class CheckpointViewModel
    {
        [Required]
        public Guid SessionId { get; set; }

        [Required]
        public Guid TrackedPlayerId { get; set; }

        [Required]
        public string CheckpointName { get; set; } = null!;

    }
}
