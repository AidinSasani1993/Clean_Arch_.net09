using Clean.Application.Dtos.Products.Requests;

namespace Clean.Application.Dtos.Categories.Requests
{
    public class CategoryProductDto
    {
        public CategoryDto Category { get; set; }
        public List<CreateProductDto> Products { get; set; }
    }
}
