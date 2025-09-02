using Clean.Application.Repositories;
using Clean.Domain.Entities.Customers;
using Clean.EntityFrameworkCore.DataBaseContext;
using Clean.Repository.Framework;

namespace Clean.Repository.Customers
{
    public class CustomerRepository : BaseRepository<CleanDbContext, Customer, long>, ICustomerRepository
    {
        public CustomerRepository(CleanDbContext context) : base(context)
        {
        }
    }
}
