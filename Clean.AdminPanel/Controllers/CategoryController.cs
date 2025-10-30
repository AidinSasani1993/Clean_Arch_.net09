using Clean.Application.Dtos.BaseDtos;
using Clean.Application.Dtos.Categories.Requests;
using Clean.Application.Dtos.Categories.Responses;
using Clean.Application.Framework;
using Clean.Application.Services.CategoryServices;
using Clean.Application.UseCase.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace Clean.AdminPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(ServiceAvailabilityActionFilter))]
    //[ProducesResponseType(typeof(ResponseResultViewModel<AgentResponseViewModel>), StatusCodes.Status200OK)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMediator _mediator;

        public CategoryController(ICategoryService categoryService, IMediator mediator)
        {
            _categoryService = categoryService;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<long> CreateAsync(CategoryDto dto)
        {
            var result = await _categoryService.CreateAsync(dto);
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] BaseFilterDto filter)
        {
            var result = await _mediator.Send(new GetCategoryQuery(filter));
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

        [HttpPost("CategoryProduct")]
        public async Task<IActionResult> CreateCategoryProductAsync(CategoryProductDto dto)
        {
            var result = await _categoryService.CreateCategoryProduct(dto);
            return Ok(result);
        }

    }
}
