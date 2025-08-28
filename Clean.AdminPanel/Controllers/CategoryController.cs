using Clean.Application.Dtos.Categories.Requests;
using Clean.Application.Dtos.Categories.Responses;
using Clean.Application.Framework;
using Clean.Application.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _categoryService.GetById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long id, CategoryDto dto)
        {
            var result = await _categoryService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpPatch("Delete/{id:long}")]
        public async Task DeleteAsync(long id)
        {
            await _categoryService.DeleteAsync(id);
        }

        [HttpPatch("Active/{id:long}")]
        public async Task ActiveAsync(long id)
        {
            await _categoryService.ActiveAsync(id);
        }
    }
}
