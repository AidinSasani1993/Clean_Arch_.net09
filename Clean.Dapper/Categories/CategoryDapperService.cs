using Clean.Application.Dtos.Categories.Responses;
using Clean.Application.Services.CategoryServices;
using Clean.Dapper.DapperDatabaseContext;
using Clean.Domain.Entities;
using Dapper;

namespace Clean.Dapper.Categories
{
    public class CategoryDapperService : ICategoryDapperService
    {
        private readonly DapperContext _dapper;

        public CategoryDapperService(DapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<GetCategoryDto>> ReadAllAsync(string? title)
        {
            var query = "Select c.Title , c.Description from Category c";
            using (var connection = _dapper.CreateConnection()) 
            {
                var company = await connection.QueryAsync<GetCategoryDto>(query);
                return company.ToList();
            }
        }
    }
}
