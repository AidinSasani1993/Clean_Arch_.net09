using Clean.Domain.Entities.People.ParaMeterObject;
using Clean.Domain.Framework;

namespace Clean.Domain.Entities.People
{
    public class Person(string firstName,string lastName,string nationalCode)
                        : Entity<long>, IActive, ISoftDeleted
    {

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NationalCode { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsDeleted { get; private set; } = false;

        public void Execute(RenameParameter param)
        {
            firstName = param.firstName.Trim();
            lastName = param.lastName.Trim();
            nationalCode = param.nationalCode.Trim();
        }


    }
}
