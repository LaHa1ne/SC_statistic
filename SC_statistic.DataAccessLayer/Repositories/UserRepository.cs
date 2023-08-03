using Microsoft.EntityFrameworkCore;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db) : base(db) 
        { 
        }

        public async Task<User> GetByLogin(string login)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<User> GetByUserId(Guid userId)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }


    }
}
