using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<Player> TrackedPlayers { get; set; } = new List<Player>();
    }
}
