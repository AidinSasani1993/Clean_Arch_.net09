namespace Clean.Application.Dtos.Products.Requests
{
    public class CreateProductDto
    {
        public long CategoryRef { get; set; }
        public string Title { get; set; }
        public decimal Fee { get; set; }
        public long Amount { get; set; }
        public string Code { get; set; }
    }
}
