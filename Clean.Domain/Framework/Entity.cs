namespace Clean.Domain.Framework
{
    public class Entity<TKey> : IModel<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
