using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.ViewModels.PlayerStatistic
{
    public class SessionViewModel
    {
        public Guid? SessionId { get; set; }
        public string SessionName { get; set; } = null!;

        [Required]
        public Guid TrackedPlayerId { get; set; }

        [Required]
        public string TrackedPlayerNickname { get; set; } = null!;

    }
}