using Clean.Application.Framework;
using Clean.Domain.Entities;

namespace Clean.Application.Repositories
{
    public interface IProductRepository : IRepository<Product, long>
    {
    }
}
