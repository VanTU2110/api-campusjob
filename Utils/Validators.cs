using System.Text.RegularExpressions;

namespace apicampusjob.Utils
{
    public class Validators
    {
        public static bool IsValidEmail(string email)
        {
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }


        public static bool IsValidPassword(string password)
        {
            if (password.Length < 8)
            {
                return false; // Mật khẩu phải có ít nhất 8 ký tự
            }

            bool hasUpperCase = Regex.IsMatch(password, @"[A-Z]");

            bool hasLowerCase = Regex.IsMatch(password, @"[a-z]");

            bool hasDigit = Regex.IsMatch(password, @"\d");

            bool hasSpecialChar = Regex.IsMatch(password, @"[\W_]");

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        // Kiểm tra số điện thoại
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            var phoneRegex = @"^\d{10}$"; // 10 chữ số
            return Regex.IsMatch(phoneNumber, phoneRegex);
        }
    }
}
