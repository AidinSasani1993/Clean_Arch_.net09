using Clean.Application.Dtos.BaseDtos;
using Clean.Application.Dtos.Customers;
using Clean.Application.Repositories;
using Clean.Application.Services.Customers;
using Clean.Common.Exceptions;
using Clean.Domain.Entities.Customers;
using System.Threading.Tasks;

namespace Clean.Service.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        #region [-ctor-]
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion

        #region [-CreateAsync(CustomerDto dto)-]
        public async Task<long> CreateAsync(CustomerDto dto)
        {
            await CheckDuplicate(0, dto.NationalCode, dto.MobileNumber);
            var customer = Customer.Create(dto.FirstName,
                                                   dto.LastName,
                                                   dto.NationalCode,
                                                   dto.BirthDay,
                                                   dto.SexType,
                                                   dto.MobileNumber);

            await _customerRepository.CreateAsync(customer);
            return customer.Id;

        } 
        #endregion

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginateViewModel<IEnumerable<CustomerDto>>> GetAllAsync(BaseFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateAsync(CustomerDto dto)
        {
            throw new NotImplementedException();
        }


        private async Task CheckDuplicate(long id, string nationalCode, string mobileNumber)
        {
            var result = _customerRepository
                .GetQueryable(disableMaxRowLimit: true)
                .Any(a => a.Id != id &&
                a.NationalCode == nationalCode ||
                a.MobileNumber == mobileNumber);
            if (result)
            {
                throw new BussinessException(ErrorMessage.CustomerTitleDuplicate);
            }
        }


    }
}
