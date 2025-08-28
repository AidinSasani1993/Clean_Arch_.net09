namespace Clean.Common.Exceptions
{
    public class ErrorMessage
    {
        public const string IdNotFount = "شناسه مورد نظر پیدا نشد";

        #region [-Category-]
        public const string CategoryTitleMaxLength = "عنوان نوع کالا نمیتواند بیشتر از {0} باشد";
        public const string CategoryTitleMinLength = "عنوان نوع کالا نمیتواند کمتر از {0} باشد";
        public const string CategoryTitleRequired = "عنوان نوع کالا نمیتواند خالی باشد";
        public const string CategoryTitleDuplicate = "عنوان نوع کالا نمیتواند تکراری باشد";
        public const string CategoryIsActive = "دسته کالا غیر فعال نیست";
        public const string CategoryIsDelete = "دسته کالا قبلا غیر فعال شده";
        public const string CategoryDescriptionMaxLength = "توضیحات نوع کالا نمیتواند بیشتر از {0} باشد"; 
        #endregion

    }
}
