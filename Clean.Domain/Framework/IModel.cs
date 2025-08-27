namespace Clean.Domain.Framework
{
    public interface IModel<TKek> where TKek : struct
    {
        public TKek Id { get; set; }
    }
}
