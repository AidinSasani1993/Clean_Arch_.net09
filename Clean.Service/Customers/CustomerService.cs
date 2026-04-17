using Clean.Application.Dtos.BaseDtos;
using Clean.Application.Dtos.Customers;
using Clean.Application.Repositories;
using Clean.Application.Services.Customers;
using Clean.Common.Exceptions;
using Clean.Domain.Entities.Customers;

namespace Clean.Service.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(10);

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
            await _customerRepository.SaveChangesAsync();
            return customer.Id;

        } 
        #endregion

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginateViewModel<IEnumerable<CustomerDto>>> GetAllAsync(BaseFilterDto filter)
        {
            var result = new PaginateViewModel<IEnumerable<CustomerDto>>();
            var skip = (filter.PageNumber - 1) * filter.PageSize;
            var customers = await _customerRepository.GetAllAsync();
            var resultData = customers.Select(a => new CustomerDto
            {
                BirthDay = a.BirthDay,
                FirstName = a.FirstName,
                LastName = a.LastName,
                SexType = a.SexType,
                MobileNumber = a.MobileNumber,
                NationalCode = a.NationalCode,
            }).ToList();
            result.Records = resultData.AsEnumerable().Skip(skip).Take(filter.PageSize);
            result.PageSize = filter.PageSize;
            result.PageNumber = filter.PageNumber;
            result.TotalCount = customers.Count();
            return result;
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

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            try
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} شروع درخواست جدید");
                var query = await _customerRepository.GetAllAsync();
                var result = query.Select(a => new CustomerDto
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    MobileNumber = a.MobileNumber,
                }).ToList();
                await Task.Delay(1000); 
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} پایان درخواست");
                return result;
            }
            finally
            {
                _semaphore.Release();
            }
        }

    }
}
