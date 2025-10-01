using Clean.Application.Framework;
using Clean.Domain.Entities.Users;

namespace Clean.Application.Repositories
{
    public interface IUserRepository : IRepository<User, long>
    {

    }
}
