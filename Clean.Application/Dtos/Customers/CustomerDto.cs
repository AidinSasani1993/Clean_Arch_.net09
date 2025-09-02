using Clean.Domain.Entities.Customers;

namespace Clean.Application.Dtos.Customers
{
    public class CustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDay { get; set; }
        public SexTypeEnum SexType { get; set; }
        public string MobileNumber { get; set; }
    }
}
