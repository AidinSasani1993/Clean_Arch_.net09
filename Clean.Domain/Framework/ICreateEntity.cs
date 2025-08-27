namespace Clean.Domain.Framework
{
    public interface ICreateEntity
    {
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        DateTimeOffset CreateDate { get; set; }
        /// <summary>
        /// ایجاد کننده
        /// </summary>
        string CreateBy { get; set; }
    }
}
