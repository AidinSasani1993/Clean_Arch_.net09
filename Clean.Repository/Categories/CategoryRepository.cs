using Clean.Application.Repositories;
using Clean.Domain.Entities;
using Clean.EntityFrameworkCore.DataBaseContext;
using Clean.Repository.Framework;

namespace Clean.Repository.Categories
{
    public class CategoryRepository : BaseRepository<CleanDbContext, Category, long>, ICategoryRepository
    {
        public CategoryRepository(CleanDbContext context) : base(context)
        {
        }
    }
}
