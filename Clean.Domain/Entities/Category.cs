using Clean.Common.Exceptions;
using Clean.Domain.Framework;

namespace Clean.Domain.Entities
{
    public class Category : Entity<long> , IActive , ISoftDeleted
    {
        #region [-Fields-]
        public const int TitleMaxLength = 100;
        public const int TitleMinLength = 3;
        public const int DescriptionMaxLength = 300;
        #endregion

        #region [-ctors-]
        protected Category()
        {
            Products = new List<Product>();
        }

        public Category(string title, string description) : this()
        {
            ValidationTitle(title);
            Title = title;
            ValidationDescription(description);
            Description = description;
        }
        #endregion

        #region [-props-]
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsDeleted { get; private set; } = false;
        public ICollection<Product> Products { get; private set; }
        #endregion

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        #region [-Create(string title, string? description)-]
        public static Category Create(string title, string? description)
        {
            ValidationTitle(title);
            ValidationDescription(description);
            var category = new Category
            {
                Title = title,
                Description = description,
            };
            return category;
        }
        #endregion

        #region [-Update(string title, string? description)-]
        public void Update(string title, string? description)
        {
            ValidationTitle(Title);
            Title = title;
            ValidationDescription(description);
            Description = description;
        } 
        #endregion

        public void SetActive()
        {
            if(!IsActive)
            {
                IsActive = true;
                IsDeleted = false;
            }
            else
            {
                throw new BussinessException(ErrorMessage.CategoryIsActive);
            }
        }

        public void SetDelete()
        {
            if (!IsDeleted)
            {
                IsDeleted = true;
                IsActive = false;
            }
            else
            {
                throw new BussinessException(ErrorMessage.CategoryIsDelete);
            }
        }

        #region [-Validation Methods-]
        private static void ValidationTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) 
            {
                throw new Exception(ErrorMessage.CategoryTitleRequired);
            }

            title = title.Trim();

            switch (title.Length)
            {
                case > TitleMaxLength:
                    throw new Exception(string.Format(ErrorMessage.CategoryTitleMaxLength, TitleMaxLength));
                case < TitleMinLength:
                    throw new Exception(string.Format(ErrorMessage.CategoryTitleMinLength,TitleMinLength));
            }
        }

        private static void ValidationDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description)) return;

            description = description.Trim();

            if (description.Length > DescriptionMaxLength) 
            {
                throw new Exception(string.Format(ErrorMessage.CategoryDescriptionMaxLength, DescriptionMaxLength));
            }
        }


        #endregion

    }
}
