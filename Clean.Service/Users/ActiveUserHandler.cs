using Clean.EntityFrameworkCore.DataBaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Clean.Service.Users
{
    public class ActiveUserHandler : AuthorizationHandler<UserRequirement>
    {
        private readonly CleanDbContext _context;

        public ActiveUserHandler(CleanDbContext context)
        {
            _context = context;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!context.User.Identity.IsAuthenticated)
            {
                return;
            }

            foreach (var claim in context.User.Claims)
            {
                Console.WriteLine($"{claim.Type} : {claim.Value}");
            }

            if (userId == null) return;

            var user = await _context.Users.FirstOrDefaultAsync(a => a.Username == userId && a.Role.Title == requirement.RoleName);
            if (user == null) return;

            if (user.IsActive && !user.IsDeleted)
            {
                context.Succeed(requirement);
            }

        }

        //public override Task HandleAsync(AuthorizationHandlerContext context)
        //{
        //    return base.HandleAsync(context);
        //}

    }
}
