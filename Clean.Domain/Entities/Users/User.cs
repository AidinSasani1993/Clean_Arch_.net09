using Microsoft.AspNetCore.Identity;

namespace Clean.Domain.Entities.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
