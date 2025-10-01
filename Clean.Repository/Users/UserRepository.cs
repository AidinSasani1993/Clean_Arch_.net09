using Clean.Application.Repositories;
using Clean.Domain.Entities.Users;
using Clean.EntityFrameworkCore.DataBaseContext;
using Clean.Repository.Framework;

namespace Clean.Repository.Users
{
    public class UserRepository : BaseRepository<CleanDbContext, User, long>, IUserRepository
    {
        public UserRepository(CleanDbContext context) : base(context)
        {
        }
    }
}
