using Clean.Application.Dtos.Customers;
using Clean.Application.Services.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.AdminPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CustomerDto dto)
        {
            var result = await _customerService.CreateAsync(dto);
            return Ok(result);
        }


    }
}
