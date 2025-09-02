using Clean.Application.Framework;
using Clean.Domain.Entities.Customers;

namespace Clean.Application.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, long>
    {
    }
}
