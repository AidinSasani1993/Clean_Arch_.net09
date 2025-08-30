using Clean.Application.Repositories;
using Clean.Domain.Entities;
using Clean.EntityFrameworkCore.DataBaseContext;
using Clean.Repository.Framework;

namespace Clean.Repository.Products
{
    public class ProductRepository : BaseRepository<CleanDbContext, Product, long>, IProductRepository
    {
        public ProductRepository(CleanDbContext context) : base(context)
        {
        }
    }
}
