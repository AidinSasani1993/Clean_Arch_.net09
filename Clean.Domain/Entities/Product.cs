using Clean.Common.Exceptions;
using Clean.Common.Extentions;
using Clean.Domain.Framework;

namespace Clean.Domain.Entities
{
    public class Product : Entity<long>, IActive, ISoftDeleted
    {

        #region [-Fields-]
        public const int TitleMaxLength = 50;
        public const int TitleMinLength = 5;
        #endregion

        #region [-ctors-]
        protected Product()
        {

        } 
        #endregion

        #region [-Props-]
        public long CategoryRef { get; private set; }
        public Category Category { get; private set; }
        public string Title { get; private set; }
        public long Amount { get; private set; }
        public decimal Fee { get; private set; }
        public string Code { get; set; }
        public bool IsActive { get; private set; } = true;
        public bool IsDeleted { get; private set; } = false;
        #endregion

        #region [-Create(string title, long amount, decimal fee, long categoryRef)-]
        public static Product Create(string title, long amount, decimal fee, long categoryRef, string code)
        {
            TitleValidatione(title);

            var product = new Product
            {
                CategoryRef = categoryRef,
                Title = title,
                Amount = amount,
                Fee = fee,
                Code = code,
            };
            return product;
        }
        #endregion

        #region [-Update(string title, long amount, decimal fee, long categoryRef)-]
        public void Update(string title, long amount, decimal fee, long categoryRef, string code)
        {
            TitleValidatione(title);
            CategoryRef = categoryRef;
            Title = title;
            Amount = amount;
            Fee = fee;
            Code = code;
        }
        #endregion

        #region [-SetDelete()-]
        public void SetDelete()
        {
            if (!IsDeleted)
            {
                IsDeleted = true;
                IsActive = false;
            }
            else
            {
                throw new BussinessException("");
            }
        } 
        #endregion

        #region [-SetActive()-]
        public void SetActive()
        {
            if (!IsActive)
            {
                IsActive = true;
                IsDeleted = false;
            }
            else
            {
                throw new BussinessException("");
            }
        }
        #endregion

        //Validations

        #region [-TitleValidatione(string title)-]
        private static void TitleValidatione(string title)
        {
            if (title is null)
            {
                throw new BussinessException("");
            }

            if (title.Length > TitleMaxLength)
            {
                throw new BussinessException("");
            }

            if (title.Length < TitleMinLength)
            {
                throw new BussinessException("");
            }

            title.NameValidation();
        } 
        #endregion

    }
}
