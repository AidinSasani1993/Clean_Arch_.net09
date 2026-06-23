using Clean.Application.Dtos.BaseDtos;
using Clean.Application.Dtos.Categories.Requests;
using Clean.Application.Services.CategoryServices;
using Clean.Application.UseCase.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading;

namespace Clean.AdminPanel.Controllers
{
    [Route("api/[controller]")]
    [RequestTimeout(2000)]
    [ApiController]
    //[Authorize(Policy = "AdminCustom")]
    //[ServiceFilter(typeof(ServiceAvailabilityActionFilter))]
    //[ProducesResponseType(typeof(ResponseResultViewModel<AgentResponseViewModel>), StatusCodes.Status200OK)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        #region [-ctor-]
        public CategoryController(ICategoryService categoryService,
                                  IMediator mediator,
                                  ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _mediator = mediator;
            _logger = logger;
        } 
        #endregion

        [HttpPost("create")]
        //[ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync(CategoryDto dto)
        {
            var result = await _categoryService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpGet("GetAll2")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> GetAll2Async(CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetAllAsync2(cancellationToken);
            return Ok(result);
        }

        [Authorize(Policy = "AdminCustom")]
        [HttpGet("GetAll")]
        [RequestTimeout("MyPolicy")]
        public async Task<IActionResult> GetAllAsync([FromQuery] BaseFilterDto filter,CancellationToken cancellationToken)
        {
            await Task.Delay(8000, cancellationToken);
            var result = await _mediator.Send(new GetCategoryQuery(filter));
            return Ok(result);
        }


        //[RequestTimeout("MyPolicy")]
        [HttpGet("timeout-test")]
        public async Task<IActionResult> TimeoutTest(CancellationToken token)
        {
            Console.WriteLine($"CanBeCanceled={token.CanBeCanceled}");

            token.Register(() =>
            {
                Console.WriteLine("TOKEN CANCELED");
            });

            await Task.Delay(10000, token);

            return Ok("Finished");
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
