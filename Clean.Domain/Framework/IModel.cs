namespace Clean.Domain.Framework
{
    public interface IModel<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
