namespace Clean.Domain.Framework
{
    public interface IBaseModel<TKek> : IModel<TKek> where TKek : struct
    {
        public byte[] RowVersion { get; set; }
    }
}
