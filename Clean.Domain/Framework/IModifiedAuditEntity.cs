namespace Clean.Domain.Framework
{
    public interface IModifiedAuditEntity
    {
        /// <summary>
        /// تاریخ آخرین تغییر
        /// </summary>
        DateTimeOffset? ModifiedDate { get; set; }
        /// <summary>
        /// تغییر دهنده
        /// </summary>
        string? ModifiedBy { get; set; }
    }
}
