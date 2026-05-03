using Clean.Domain.Entities.Customers;

namespace Clean.Application.Dtos.Users
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public SexTypeEnum SexType { get; set; }
        public bool IsActive { get; set; }
        public long? Role { get; set; }
    }
}
