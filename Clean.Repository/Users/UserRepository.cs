using Clean.Application.Repositories;
using Clean.Domain.Entities.Users;
using Clean.EntityFrameworkCore.DataBaseContext;
using Clean.Repository.Framework;
using Microsoft.EntityFrameworkCore;

namespace Clean.Repository.Users
{
    public class UserRepository : BaseRepository<CleanDbContext, User, long>, IUserRepository
    {
        public UserRepository(CleanDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string Email)
        {
            var query = await Db_Set.FirstOrDefaultAsync(a => a.Email == Email);
            return query;
        }

    }
}
