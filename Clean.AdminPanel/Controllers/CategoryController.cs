using Clean.Application.Dtos.Categories.Requests;
using Clean.Application.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Clean.AdminPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<long> CreateAsync(CategoryDto dto)
        {
            var result = await _categoryService.CreateAsync(dto);
            return result;
        }

    }
}
