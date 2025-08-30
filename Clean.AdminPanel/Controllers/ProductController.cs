using Clean.Application.Dtos.Products.Requests;
using Clean.Application.Services.ProductSevices;
using Microsoft.AspNetCore.Mvc;

namespace Clean.AdminPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productServices;

        public ProductController(IProductService productServices)
        {
            _productServices = productServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductDto dto)
        {
            var result = await _productServices.CreateAsync(dto);
            return Ok(result);
        }

    }
}
