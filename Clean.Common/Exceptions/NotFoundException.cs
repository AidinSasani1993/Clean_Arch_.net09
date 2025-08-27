namespace Clean.Common.Exceptions
{
    public class NotFoundException : FrameworkException
    {
        public NotFoundException(string? message) : base(message)
        {
        }
    }
}
