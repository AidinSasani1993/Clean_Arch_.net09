using Clean.Domain.Entities.Users;
using Clean.Domain.Framework;

namespace Clean.Domain.Entities.Roles
{
    public class Role : Entity<long>
    {
        public Role(string title)
        {
            Title = title;
        }

        public ICollection<User> Users { get; private set; }
        public string Title { get; private set; }

        public void Update(string title)
        {
            Title = title;
        }

    }
}
