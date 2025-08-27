namespace Clean.Domain.Framework
{
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; }
    }
}
