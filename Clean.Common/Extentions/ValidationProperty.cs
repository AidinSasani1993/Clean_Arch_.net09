using Clean.Common.Exceptions;
using System.Text.RegularExpressions;

namespace Clean.Common.Extentions
{
    public static class ValidationProperty
    {
        #region [-NameValidation(this string name)-]
        public static void NameValidation(this string name)
        {
            var contract = Regex.IsMatch(name, @"^[\p{L}\s]+$");
            if (!contract)
            {
                throw new BussinessException("نام فقط حروف باشد");
            }
        } 
        #endregion
    }
}
