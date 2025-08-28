namespace Clean.Application.Dtos.BaseDtos
{
    public record BaseFilterDto
    {
        private int _pageNumber;
        private int _pageSize = 20;

        public int PageNumber
        {
            get => Math.Max(1, _pageNumber);
            set => _pageNumber = value;
        }

        public int PageSize
        {
            get => Math.Min(200, _pageSize);
            set => _pageSize = value;
        }
        public string? OrderBy { get; set; }
        public bool? IsAsc { get; set; } = false;
    }
}
