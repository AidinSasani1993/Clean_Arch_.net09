using Clean.Common.Exceptions;
using Clean.Common.Extentions;
using Clean.Domain.Entities.Customers;
using Clean.Domain.Entities.Roles;
using Clean.Domain.Framework;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Clean.Domain.Entities.Users
{
    public class User : Entity<long>, IActive, ISoftDeleted
    {
        #region [-Fields-]
        public const int UserNameMaxLength = 50;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 10;
        public const int MobileNumberLength = 11;
        public const int FirstNameMaxLength = 50;
        public const int LastNameMaxLength = 50;
        public const int PhoneNumberMaxLength = 12;
        //public const int NationalCodeMaxLength = 10;
        public const int DescriptionMaxLength = 300;


        /// <summary>
        /// Validating minimum 8 characters, at-least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character
        /// https://www.aspsnippets.com/Articles/4001/Implement-Password-Policy-using-Regular-Expressions-in-C-and-VBNet/
        /// </summary>
        public const string PasswordComplexityRequirementsRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,8}";
        #endregion

        #region [-Props-]
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDay { get; private set; }
        public string PhoneNumber { get; private set; }
        public SexTypeEnum SexType { get; private set; }
        public DateTime CreateDate { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsDeleted { get; private set; } = false;
        public long RoleRef { get; private set; }
        public Role Role { get; private set; }
        #endregion

        #region [-ctors-]
        protected User()
        {
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }

        public User(string username,
                    string email,
                    string password,
                    string firstName,
                    string lastName,
                    DateTime birthDay,
                    string phoneNumber,
                    SexTypeEnum sexType,
                    bool isActive) : this()
        {
            Username = username;
            Email = email;
            ChangePassword(password);
            Password = password;
            ValidationFirstName(firstName);
            FirstName = firstName;
            ValidationLastName(lastName);
            LastName = lastName;
            BirthDay = birthDay;
            ValidationPhoneNumber(phoneNumber);
            PhoneNumber = phoneNumber;
            SexType = sexType;
            IsActive = isActive;
        } 
        #endregion

        #region [-GetUserPassHash(string plainText)-]
        public static string GetUserPassHash(string plainText)
        {
            byte[] salt = System.Text.Encoding.UTF8.GetBytes("cryptographically");

            // 44 characters
            string hashed
                = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainText!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
        #endregion

        #region [-ChengedPassword(string password)-]
        public void ChangePassword(string password)
        {
            ValidationPassword(password);
            Password = GetUserPassHash(password);
        }
        #endregion

        #region [-Validations-]
        private void ValidationPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new FrameworkException("رمز عبور نمیتواند خالی باشد");
            }
            if (password.Length > PasswordMaxLength)
            {
                throw new FrameworkException($"حداکثر طول رمز عبور باید {PasswordMaxLength} کاراکتر می باشد");
            }
            if (password.Length < PasswordMinLength)
            {
                throw new FrameworkException($"حداقل طول رمز عبور باید {PasswordMinLength} کاراکتر می باشد");
            }
        }

        private void ValidationFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FrameworkException("نام نمیتواند خالی باشد");
            }
            if (firstName.Length > FirstNameMaxLength)
            {
                throw new FrameworkException($"نام نمیتواند بیشتر از {FirstNameMaxLength} کاراکتر باشد");
            }

            bool result = firstName.IsJustPersianWord();

            if (!result)
            {
                throw new FrameworkException("نام باید فارسی باشد");
            }
        }

        private void ValidationLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new FrameworkException("نام خانوادگی نمیتواند خالی باشد");
            }
            if (lastName.Length > LastNameMaxLength)
            {
                throw new FrameworkException($"نام خانوادگی نمیتواند بیشتر از {LastNameMaxLength} کاراکتر باشد");
            }
            bool result = lastName.IsJustPersianWord();

            if (!result)
            {
                throw new FrameworkException("نام خانوادگی باید فارسی باشد");
            }
        }

        private void ValidationPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new FrameworkException("شماره تلفن نمیتواند خالی باشد");
            }

            var result = long.TryParse(phoneNumber, out _);

            if (!result)
            {
                throw new FrameworkException("شماره تلفن باید از نوع عددی باشد");
            }

            if (phoneNumber.Length > PhoneNumberMaxLength)
            {
                throw new FrameworkException("شماره تلفن حداکثر 12 رقم باشد ");
            }
        }

        #endregion
    }
}