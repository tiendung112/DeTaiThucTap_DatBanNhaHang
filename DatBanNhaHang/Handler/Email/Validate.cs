using System.ComponentModel.DataAnnotations;

namespace DatBanNhaHang.Handler.Email
{
    public class Validate
    {
        public static bool IsValidEmail(string email)
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(email);
        }
    }
}
