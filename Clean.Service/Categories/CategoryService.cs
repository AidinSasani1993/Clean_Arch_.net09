using Clean.Application.Dtos.BaseDtos;
using Clean.Application.Dtos.Categories.Requests;
using Clean.Application.Dtos.Categories.Responses;
using Clean.Application.Repositories;
using Clean.Application.Services.CategoryServices;
using Clean.Common.Exceptions;
using Clean.Domain.Entities;

namespace Clean.Service.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task ActiveAsync(long id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException(ErrorMessage.IdNotFount);
            }
            category.SetActive();
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<long> CreateAsync(CategoryDto dto)
        {
            await CheckDuplicate(0, dto);
            var category = Category.Create(dto.Title, dto.Description);
            await _categoryRepository.CreateAsync(category);
            return category.Id;
        }

        public async Task DeleteAsync(long id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException(ErrorMessage.IdNotFount);
            }

            //await _categoryRepository.DeleteAsync(id);

            category.SetDelete();
            await _categoryRepository.SaveChangesAsync();

        }

        public async Task<PaginateViewModel<IEnumerable<GetCategoryDto>>> GetAllAsync(BaseFilterDto dto)
        {
            var result =
                new PaginateViewModel<IEnumerable<GetCategoryDto>>();
            var skip = (dto.PageNumber - 1) * dto.PageSize;
            var query = await _categoryRepository.GetAllAsync();
            var category = query.Select(a => new GetCategoryDto()
            {
                Title = a.Title,
                Description = a.Description,
            }).ToList();

            result.Records = category.Skip(skip).Take(dto.PageSize).ToList();
            result.TotalCount = category.Count;
            result.PageNumber = dto.PageNumber;
            result.PageSize = dto.PageSize;

            return result; 
        }

        public async Task<GetCategoryDto> GetById(long id)
        {
            if (id == null)
            {
                throw new NotFoundException(ErrorMessage.IdNotFount);
            }
            
            var query = await _categoryRepository.GetByIdAsync(id);

            var category = new GetCategoryDto
            {
                Title = query.Title,
                Description = query.Description,
            };

            return category;
        }

        public async Task<long> UpdateAsync(long id, CategoryDto dto)
        {
            await CheckDuplicate(id, dto);
            var category = await _categoryRepository.GetByIdAsync(id);
            category.Update(category.Title, dto.Description);
            await _categoryRepository.SaveChangesAsync();
            return category.Id;
        }

        public async Task<long> CreateCategoryProduct(CategoryProductDto dto)
        {
            var category = Category.Create(dto.Category.Title, dto.Category.Description);

            foreach (var i in dto.Products)
            {
                category.AddProduct(Product.Create(i.Title, i.Amount, i.Fee, i.CategoryRef, i.Code));
            }

            await _categoryRepository.CreateAsync(category);

            return category.Id;

        }

        //Validations

        private async Task CheckDuplicate(long id, CategoryDto dto)
        {
            var category = _categoryRepository
                .GetQueryable(disableMaxRowLimit: true, disableTracking: false)
                .Any(c => c.Id != id && c.Title == dto.Title.Trim());
            
            if (category)
                throw new BussinessException(ErrorMessage.CategoryTitleDuplicate);
        }


    }
}
