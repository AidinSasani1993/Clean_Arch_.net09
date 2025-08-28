namespace Clean.Application.Dtos.BaseDtos
{
    public class PaginateViewModel<TData> where TData : class
    {
        public TData Records { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
