using Microsoft.AspNetCore.Authorization;
using System.Reflection.PortableExecutable;

namespace Clean.Service.Users
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; set; }
    }
}
