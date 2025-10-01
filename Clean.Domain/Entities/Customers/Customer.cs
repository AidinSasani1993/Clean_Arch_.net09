using Clean.Common.Exceptions;
using Clean.Common.Extentions;
using Clean.Domain.Framework;
using System.ComponentModel.DataAnnotations;

namespace Clean.Domain.Entities.Customers
{
    public class Customer : Entity<long>, IActive, ISoftDeleted
    {
        #region [-Fields-]
        public const int FirstNameMaxLength = 50;
        public const int FirstNameMinLength = 3;

        public const int LastNameMaxLength = 50;
        public const int LastNameMinLength = 3;

        public const int NationalCodeMaxLength = 12;
        public const int NationalCodeMinLength = 10;
        #endregion

        #region [-ctors-]
        protected Customer()
        {

        } 
        #endregion

        #region [-Props-]
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NationalCode { get; private set; }
        public DateTime BirthDay { get; private set; }
        public SexTypeEnum SexType { get; private set; }
        public string MobileNumber { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsDeleted { get; private set; } = false;
        #endregion

        #region [-Create-]
        public static Customer Create(string firstName,
                                      string lastName,
                                      string nationalCode,
                                      DateTime birthDay,
                                      SexTypeEnum sexType,
                                      string mobileNumber)
        {
            ValidationFirstName(firstName);
            ValidationLastName(lastName);
            ValidationBirthDay(birthDay);
            nationalCode.VerifyRealNationalNumber();

            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDay = birthDay,
                MobileNumber = mobileNumber,
                NationalCode = nationalCode,
                SexType = sexType,
            };

            return customer;
        }
        #endregion

        #region [-Update-]
        public void Update(string firstName,
                                      string lastName,
                                      string nationalCode,
                                      DateTime birthDay,
                                      SexTypeEnum sexType,
                                      string mobileNumber)
        {
            ValidationFirstName(firstName);
            ValidationLastName(lastName);
            ValidationBirthDay(birthDay);
            nationalCode.VerifyRealNationalNumber();

            FirstName = firstName;
            LastName = lastName;
            BirthDay = birthDay;
            MobileNumber = mobileNumber;
            NationalCode = nationalCode;
            SexType = sexType;
        } 
        #endregion


        //Validations

        private static void ValidationFirstName(string firstName)
        {
            firstName = firstName.Trim();
            if (firstName.Length > FirstNameMaxLength) 
            {
                throw new BussinessException("");
            }

            if (firstName.Length < FirstNameMinLength)
            {
                throw new BussinessException("");
            }

            bool result = firstName.IsJustPersianWord();
            if (!result)
            {
                throw new ValidationException("");
            }

        }

        private static void ValidationLastName(string lastName)
        {
            lastName = lastName.Trim();
            if (lastName.Length > LastNameMaxLength)
            {
                throw new BussinessException("");
            }

            if (lastName.Length < LastNameMinLength)
            {
                throw new BussinessException("");
            }

            bool result = lastName.IsJustPersianWord();
            if (!result)
            {
                throw new ValidationException("");
            }

        }

        private static void ValidationBirthDay(DateTime birthDay)
        {
            var day = DateTime.Now.Year - birthDay.Year;

            if (day < 18) 
            {
                throw new BussinessException("");
            }
        }

    }
}
